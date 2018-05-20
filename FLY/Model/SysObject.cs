namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class SysObject
    {
        private SysForm from;
        private int fromId;
        private string name;
        private int obj_ID;
        private string objctPro;
        private string objProValue;
        private Role sysRole;
        private string txtName;

        public SysObject()
        {
            this.From = new SysForm();
            this.SysRole = new Role();
        }

        public bool getSomeObjct(SysObject obj)
        {
            return (this.FromId == obj.FromId);
        }

        public SysForm From
        {
            get
            {
                return this.from;
            }
            set
            {
                this.from = value;
            }
        }

        public int FromId
        {
            get
            {
                return this.fromId;
            }
            set
            {
                this.fromId = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public int Obj_ID
        {
            get
            {
                return this.obj_ID;
            }
            set
            {
                this.obj_ID = value;
            }
        }

        public string ObjctPro
        {
            get
            {
                return this.objctPro;
            }
            set
            {
                this.objctPro = value;
            }
        }

        public string ObjProValue
        {
            get
            {
                return this.objProValue;
            }
            set
            {
                this.objProValue = value;
            }
        }

        public Role SysRole
        {
            get
            {
                return this.sysRole;
            }
            set
            {
                this.sysRole = value;
            }
        }

        public string TxtName
        {
            get
            {
                return this.txtName;
            }
            set
            {
                this.txtName = value;
            }
        }
    }
}

