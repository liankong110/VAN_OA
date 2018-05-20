namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class Role
    {
        private bool ifSelected;
        private int rID;
        private string roleCode;
        private string roleDepCode;
        private string roleMemo;
        private string roleName;
        private string roleStatus;

        public bool IfSelected
        {
            get
            {
                return this.ifSelected;
            }
            set
            {
                this.ifSelected = value;
            }
        }

        public int RID
        {
            get
            {
                return this.rID;
            }
            set
            {
                this.rID = value;
            }
        }

        public string RoleCode
        {
            get
            {
                return this.roleCode;
            }
            set
            {
                this.roleCode = value;
            }
        }

        public string RoleDepCode
        {
            get
            {
                return this.roleDepCode;
            }
            set
            {
                this.roleDepCode = value;
            }
        }

        public string RoleMemo
        {
            get
            {
                return this.roleMemo;
            }
            set
            {
                this.roleMemo = value;
            }
        }

        public string RoleName
        {
            get
            {
                return this.roleName;
            }
            set
            {
                this.roleName = value;
            }
        }

        public string RoleStatus
        {
            get
            {
                return this.roleStatus;
            }
            set
            {
                this.roleStatus = value;
            }
        }
    }
}

