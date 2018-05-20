namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class AllObjs
    {
        private string chinese;
        private string english;
        private int formId;

        public bool getForms(AllObjs obj)
        {
            return (this.FormId == obj.FormId);
        }

        public string Chinese
        {
            get
            {
                return this.chinese;
            }
            set
            {
                this.chinese = value;
            }
        }

        public string English
        {
            get
            {
                return this.english;
            }
            set
            {
                this.english = value;
            }
        }

        public int FormId
        {
            get
            {
                return this.formId;
            }
            set
            {
                this.formId = value;
            }
        }
    }
}

