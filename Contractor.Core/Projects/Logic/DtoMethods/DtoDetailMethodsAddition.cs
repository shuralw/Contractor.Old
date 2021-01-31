﻿using Contractor.Core.Helpers;
using Contractor.Core.Jobs.DomainAddition;
using Contractor.Core.Tools;
using System.IO;

namespace Contractor.Core.Projects.Persistence
{
    public class DtoDetailMethodsAddition
    {
        public PathService pathService;

        public DtoDetailMethodsAddition(PathService pathService)
        {
            this.pathService = pathService;
        }

        public void Add(PropertyOptions options, string domainFolder, string templateFileName)
        {
            string filePath = GetFilePath(options, domainFolder, templateFileName);
            string fileData = UpdateFileData(options, filePath);

            File.WriteAllText(filePath, fileData);
        }

        private string GetFilePath(PropertyOptions options, string domainFolder, string templateFileName)
        {
            string absolutePathForDTOs = this.pathService.GetAbsolutePathForDTOs(options, domainFolder);
            string fileName = templateFileName.Replace("Entity", options.EntityName);
            string filePath = Path.Combine(absolutePathForDTOs, fileName);
            return filePath;
        }

        private string UpdateFileData(PropertyOptions options, string filePath)
        {
            string fileData = File.ReadAllText(filePath);

            // ----------- DbSet -----------
            StringEditor stringEditor = new StringEditor(fileData);
            stringEditor.NextThatContains("FromDb" + options.EntityName);
            stringEditor.Next(line => line.Trim().Equals("};"));

            stringEditor.InsertLine($"                {options.PropertyName} = db{options.EntityName}.{options.PropertyName},");
            
            return stringEditor.GetText();
        }
    }
}