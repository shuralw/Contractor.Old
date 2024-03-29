﻿using Contractor.Core.Helpers;
using Contractor.Core.Jobs;
using System;
using System.IO;

namespace Contractor.Core.Tools
{
    public class EntityCoreDependencyProvider
    {
        public PathService pathService;

        public EntityCoreDependencyProvider(PathService pathService)
        {
            this.pathService = pathService;
        }

        public void UpdateDependencyProvider(IEntityAdditionOptions options, string projectFolder, string fileName)
        {
            string filePath = GetFilePath(options, projectFolder, fileName);
            string fileData = UpdateFileData(options, filePath, projectFolder);

            CsharpClassWriter.Write(filePath, fileData);
        }

        private string GetFilePath(IEntityAdditionOptions options, string projectFolder, string fileName)
        {
            return Path.Combine(options.BackendDestinationFolder, options.ProjectName + projectFolder, fileName);
        }

        private string UpdateFileData(IEntityAdditionOptions options, string filePath, string projectFolder)
        {
            string fileData = File.ReadAllText(filePath);

            fileData = AddServices(fileData, options, projectFolder);

            return fileData;
        }

        private string AddServices(string fileData, IEntityAdditionOptions options, string projectFolder)
        {
            string contractNamespace = GetContractNamespace(options, projectFolder);
            fileData = UsingStatements.Add(fileData, contractNamespace);

            string projectNamespace = GetProjectNamespace(options, projectFolder);
            fileData = UsingStatements.Add(fileData, projectNamespace);

            // Insert into Startup-Method
            StringEditor stringEditor = new StringEditor(fileData);
            string addScopedStatement = GetAddScopedStatement(options.EntityNamePlural, projectFolder);
            stringEditor.NextThatContains($"void Startup{options.Domain}");
            stringEditor.Next();
            stringEditor.Next(line => line.CompareTo(addScopedStatement) > 0 || line.Contains("}"));
            stringEditor.InsertLine(addScopedStatement);

            return stringEditor.GetText();
        }

        private string GetContractNamespace(IEntityAdditionOptions options, string projectFolder)
        {
            if (projectFolder.Equals(".Logic"))
            {
                return $"{options.ProjectName}.Contract.Logic.Model.{options.Domain}.{options.EntityNamePlural}";
            }
            else if (projectFolder.Equals(".Persistence"))
            {
                return $"{options.ProjectName}.Contract.Persistence.Model.{options.Domain}.{options.EntityNamePlural}";
            }

            throw new ArgumentException("Argument 'projectFolder' invalid");
        }

        private string GetProjectNamespace(IEntityAdditionOptions options, string projectFolder)
        {
            if (projectFolder.Equals(".Logic"))
            {
                return $"{options.ProjectName}.Logic.Model.{options.Domain}.{options.EntityNamePlural}";
            }
            else if (projectFolder.Equals(".Persistence"))
            {
                return $"{options.ProjectName}.Persistence.Model.{options.Domain}.{options.EntityNamePlural}";
            }

            throw new ArgumentException("Argument 'projectFolder' invalid");
        }

        private string GetAddScopedStatement(string entityNamePlural, string projectFolder)
        {
            if (projectFolder.Equals(".Logic"))
            {
                return $"            services.AddScoped<I{entityNamePlural}CrudLogic, {entityNamePlural}CrudLogic>();";
            }
            else if (projectFolder.Equals(".Persistence"))
            {
                return $"            services.AddScoped<I{entityNamePlural}CrudRepository, {entityNamePlural}CrudRepository>();";
            }

            throw new ArgumentException("Argument 'projectFolder' invalid");
        }
    }
}