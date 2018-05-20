namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class SysForm
    {
        private string assemblyPath;
        private string displayName;
        private int formID;
        private string formImgURL;
        private int formIndex;
        private SysMenu menu;
        private int upperID;

        public SysForm()
        {
            this.Menu = new SysMenu();
        }

        public bool getAllForms(SysForm form)
        {
            return (form.DisplayName.IndexOf(this.DisplayName) != -1);
        }

        public bool getForms(SysForm form)
        {
            return (this.UpperID == form.UpperID);
        }

        public string AssemblyPath
        {
            get
            {
                return this.assemblyPath;
            }
            set
            {
                this.assemblyPath = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
            set
            {
                this.displayName = value;
            }
        }

        public int FormID
        {
            get
            {
                return this.formID;
            }
            set
            {
                this.formID = value;
            }
        }

        public string FormImgURL
        {
            get
            {
                return this.formImgURL;
            }
            set
            {
                this.formImgURL = value;
            }
        }

        public int FormIndex
        {
            get
            {
                return this.formIndex;
            }
            set
            {
                this.formIndex = value;
            }
        }

        public SysMenu Menu
        {
            get
            {
                return this.menu;
            }
            set
            {
                this.menu = value;
            }
        }

        public int UpperID
        {
            get
            {
                return this.upperID;
            }
            set
            {
                this.upperID = value;
            }
        }
    }
}

