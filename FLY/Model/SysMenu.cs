namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class SysMenu
    {
        private string displayName;
        private int iconIndex;
        private int menuID;
        private int menuIndex;

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

        public int IconIndex
        {
            get
            {
                return this.iconIndex;
            }
            set
            {
                this.iconIndex = value;
            }
        }

        public int MenuID
        {
            get
            {
                return this.menuID;
            }
            set
            {
                this.menuID = value;
            }
        }

        public int MenuIndex
        {
            get
            {
                return this.menuIndex;
            }
            set
            {
                this.menuIndex = value;
            }
        }
    }
}

