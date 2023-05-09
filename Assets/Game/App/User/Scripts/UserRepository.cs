namespace Game.App
{
    public sealed class UserRepository : DataRepository<UserData>
    {
        protected override string Key => "UserData";

        public bool LoadUser(out UserData data)
        {
            return this.LoadData(out data);
        }

        public void SaveUser(UserData data)
        {
            this.SaveData(data);
        }
    }
}