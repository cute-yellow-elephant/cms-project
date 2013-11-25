using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;
using AppCore;
using Domain;

namespace ApointMvcApp.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override bool ValidateUser(string emailOrLogin, string password)
        {
            bool isValid = false;
            try
            {
                var core = new CoreHolder();
                var userByEmail = core.UserRepository.FindByEmail(emailOrLogin);
                var user = userByEmail != null ? userByEmail : core.UserRepository.Find(emailOrLogin);
                if (user == null)
                    throw new Exception("Пользователя с таким логином/почтой не существует");
                if(!Crypto.VerifyHashedPassword(user.Password, password))
                    throw new Exception("Введен неверный пароль");
                if (user.IsDeleted)
                    throw new Exception("Пользователь с такими данными удален");
                if (!user.IsVerified)
                    throw new Exception("Пользователь еще не прошел проверку через почту");
                user.LastVisitDate = DateTime.UtcNow;
                core.Submit();
                isValid = true;
            }
            catch (Exception e) { throw new Exception(e.Message); }
            return isValid;
        }

        public MembershipUser CreateUser(string login, string email, string password)
        {
            try
            {
                MembershipUser membershipUser = GetUser(login, false) == null ? GetUser(email, false) : null;
                if (membershipUser != null) throw new Exception("Пользователь с таким логином или почтой уже существует.");
                var core = new CoreHolder();
                var ShouldCreateAdmin = core.UserRepository.ShouldCreateAdmin();
                core.UserRepository.Create(login, email, Crypto.HashPassword(password), DateTime.UtcNow, DateTime.UtcNow, false,false);
                core.Submit();
                if (ShouldCreateAdmin)
                    core.RoleRepository.Find("Admin").Users.Add(core.UserRepository.Find(login));
                core.RoleRepository.Find("User").Users.Add(core.UserRepository.Find(login));
                core.Submit();
                membershipUser = GetUser(email, false);
                return membershipUser;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser memberUser;
            try
            {
                var core = new CoreHolder();
                var user = core.UserRepository.FindByEmail(username);
                if (user == null) user = core.UserRepository.Find(username);
                if (userIsOnline)
                {
                    user.LastVisitDate = DateTime.UtcNow;
                    core.Submit();
                }
                memberUser = new MembershipUser("CustomMembershipProvider", user.Email, null, user.Email, null, null,
                   user.IsVerified, user.IsDeleted, user.AddedDate, DateTime.MinValue, user.LastVisitDate, DateTime.MinValue, DateTime.MinValue);
            }
            catch { return null; }
            return memberUser;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                var core = new CoreHolder();
                var user = core.UserRepository.Find(username) ?? core.UserRepository.FindByEmail(username);
                if (user.IsOnline)
                    throw new Exception(String.Format("Пользователь {0} онлайн. Попробуйте провести удаление позже.", username));
                core.UserRepository.Delete(user.ID);
                core.Submit();
                return true;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            try
            {
                var core = new CoreHolder();
                var userList = core.UserRepository.ReadAll();
                var userMembershipList = new MembershipUserCollection();
                foreach (var user in userList)
                    userMembershipList.Add( new MembershipUser("CustomMembershipProvider", user.Login, null, user.Email, null, null,
                   user.IsVerified, user.IsDeleted, user.AddedDate, DateTime.MinValue,user.LastVisitDate, DateTime.MinValue, DateTime.MinValue));
                totalRecords = userMembershipList.Count;
                return userMembershipList;
            }
            catch { totalRecords = 0; return null; }
        }

        public override int GetNumberOfUsersOnline()
        {
            int numberOnline = 0;
            var core = new CoreHolder();
            int userIsOnlineTimeWindow = 20;
            if (core.UserRepository.ReadAll() == null) return 0;
            foreach (var x in core.UserRepository.ReadAll())
            {
                TimeSpan span = DateTime.UtcNow - x.LastVisitDate;
                if (span.Minutes <= userIsOnlineTimeWindow) {x.IsOnline = true; numberOnline++;}
                else x.IsOnline = false;
            }
            core.Submit();
            return numberOnline;             
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }
        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
    }    
}