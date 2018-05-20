namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class ShowAccordion
    {
        private string body;
        private string heardText;

        public string Body
        {
            get
            {
                return this.body;
            }
            set
            {
                this.body = value;
            }
        }

        public string HeardText
        {
            get
            {
                return this.heardText;
            }
            set
            {
                this.heardText = value;
            }
        }
    }
}

