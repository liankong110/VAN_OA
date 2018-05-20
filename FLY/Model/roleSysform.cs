namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class roleSysform
    {
        private VAN_OA.Model.Role role;
        private SysForm sysform;

        public roleSysform()
        {
            this.Role = new VAN_OA.Model.Role();
            this.Sysform = new SysForm();
        }

        public VAN_OA.Model.Role Role
        {
            get
            {
                return this.role;
            }
            set
            {
                this.role = value;
            }
        }

        public SysForm Sysform
        {
            get
            {
                return this.sysform;
            }
            set
            {
                this.sysform = value;
            }
        }
    }
}

