using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TodoList.BusinessLayer.Contracts;
using TodoList.BusinessLayer.DTOs;
using TodoList.DataLayer.Context;
using TodoList.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TodoList.BusinessLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private readonly TodoListContext _context;

        public AccountRepository(TodoListContext context)
        {
            _context = context;
        }

        public UserDTO AuthenticateUser(LoginDTO loginDTO)
        {
            var user = _context.Users.FirstOrDefault(p => p.UserName == loginDTO.UserName && p.Password == loginDTO.Password);
            if (user != null)
            {
                var userDTO = new UserDTO()
                {
                    ID = user.ID,
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email
                };
                return userDTO;
            }
            else
            {
                return null;
            }

        }

        public UserDTO RegisterUser(UserDTO userDTO)
        {
            try
            {
                var User = new User()
                {
                    UserName = userDTO.UserName,
                    Password = userDTO.Password,
                    Email = userDTO.Email,
                };
                _context.Users.Add(User);
                _context.SaveChanges();
                userDTO.ID = User.ID;
                return userDTO;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
