namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class roleSysObject
    {
        private VAN_OA.Model.Role role;
        private SysObject sysObject;

        public roleSysObject()
        {
            this.Role = new VAN_OA.Model.Role();
            this._SysObject = new SysObject();
        }

        public SysObject _SysObject
        {
            get
            {
                return this.sysObject;
            }
            set
            {
                this.sysObject = value;
            }
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
    }
}

