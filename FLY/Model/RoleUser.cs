namespace VAN_OA.Model
{
    using System;

    [Serializable]
    public class RoleUser
    {
        private VAN_OA.Model.Role role;
        private int roleId;
        private User user;
        private int userId;

        public RoleUser()
        {
            this.Role = new VAN_OA.Model.Role();
            this._User = new User();
        }

        public User _User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
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

        public int RoleId
        {
            get
            {
                return this.roleId;
            }
            set
            {
                this.roleId = value;
            }
        }

        public int UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }
    }
}

