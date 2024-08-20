using Linkout.DAL;
using Linkout.Models;

namespace Linkout.Services
{
    public class UserService
    {
        private DataLayer db;
        public UserService(DataLayer _db) { db = _db; }

        public async Task <UserModel> getUserById(int id)
        {
            return db.Users.Find(id);
        }         
        public async Task <UserModel> getUserByUserNameAndPassword (string un,string uhpw)
        {
            return db.Users.FirstOrDefault(u=>u.username == un && u.UNHASHEDpassword == null);
        }        
        public async Task <int> register(UserModel user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            UserModel created = db.Users.FirstOrDefault(U=>U.username == user.username);
            return created.id;

        }
    }
}
