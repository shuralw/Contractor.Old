namespace Contractor.CLI.Metamodell
{
    public abstract class MemberElement : AnnotatedElement
    {

        private VisibilityEnum visibility = VisibilityEnum.Private;

        public VisibilityEnum GetVisibility()
        {
            return this.visibility;
        }

        public void SetVisibility(VisibilityEnum visibility)
        {
            this.visibility = visibility;
        }
    }
}
