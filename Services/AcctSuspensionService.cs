﻿using Microsoft.AspNetCore.Identity;
using OnAccount.Areas.Identity.Data;
using System;
/*
 This service runs once per hour to update scheduled account lockout events.
 */
namespace OnAccount.Services
{
    public class AcctSuspensionService : IHostedService, IDisposable
    {
        private Timer? _timer;
        private readonly UserManager<AppUser> _userManager;
        
        private DbConnectorService _dbConnectorService;
        public AcctSuspensionService(UserManager<AppUser> userManager) 
        {
            this._userManager = userManager;
            this._dbConnectorService = new DbConnectorService();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(864000)); //runs once perday from the last start time.
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
        private void DoWork(object? state)
        {
            UpdateLockouts();
            UpdateLockoutsExpPassword();
        }
        private void UpdateLockouts()
        {
            var users = this._userManager.Users.ToList();
            for (var i =0; i < users.Count; i++)
            {
                if (users[i].AcctSuspensionDate >= System.DateTime.Now && users[i].AcctReinstatementDate >= System.DateTime.Now)
                {
                    _dbConnectorService.UpdateLockout(users[i]);
                }
            }
        }
        private async void UpdateLockoutsExpPassword()
        {
            var users = this._userManager.Users.ToList();
            for (var i = 0; i < users.Count; i++)
            {
                DateTime lastChangedDate = (DateTime)users[i].LastPasswordChangedDate;
                var nextExpiration = lastChangedDate.AddDays(Int32.Parse(users[i].PasswordResetDays));
                if (nextExpiration <= System.DateTime.Now)
                {
                    _dbConnectorService.UpdateLockout(users[i]);
                    Services.EmailService sendit = new EmailService();
                    var subject = "Alert! Your OnAccount password is about to expire! (Sent on behalf of On-Account from Magnadigi.com";
                    var body = "Dear " + users[i].FirstName + ", \n" + "Your password expires on " + nextExpiration + ".\n" + "Please vist https://www.on-account.net to update your password. \nThank You,\nThe on-account team.";
                    await sendit.SendEmailAsync(users[i].Email, subject, body);
                }
            }
        }
        
    }
}
