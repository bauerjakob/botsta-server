﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Botsta.DataStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Botsta.DataStorage
{
    public class BotstaDbRepository : IBotstaDbRepository
    {
        private BotstaDbContext _dbContext;

        public BotstaDbRepository(BotstaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserToDb(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<Message> GetMessages(string chatroomId)
        {
            return _dbContext.Chatrooms?
                .Single(c => c.Id.ToString() == chatroomId).Messages;
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users?
                .Include(u => u.Chatrooms)
                .Include(u => u.Bots)
                .Single(u => u.Username == username);
        }

        public User GetUserById(string userId)
        {
            return _dbContext.Users?
                .Include(u => u.Chatrooms)
                .Include(u => u.Bots)
                .Single(u => u.Id.ToString() == userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public Bot GetBotById(string botId)
        {
            return _dbContext.Bots.Single(b => b.Id.ToString() == botId);
        }

        public async Task AddChatroomToDbAsync(Chatroom chatroom)
        {
            if (chatroom is null)
            {
                throw new ArgumentNullException(nameof(chatroom));
            }

            await _dbContext.Chatrooms.AddAsync(chatroom);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddMessageToDb(Message message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }

        public Bot GetBotByName(string botName)
        {
            return _dbContext.Bots.Single(b => b.BotName == botName);
        }

        public async Task AddBotToDbAsync(Bot bot)
        {
            if (bot is null)
            {
                throw new ArgumentNullException(nameof(bot));
            }

            await _dbContext.Bots.AddAsync(bot);
            await _dbContext.SaveChangesAsync();
        }
    }
}