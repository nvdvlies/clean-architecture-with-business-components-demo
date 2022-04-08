﻿using Demo.Domain.Shared.DomainEntity;
using Demo.Domain.Shared.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Domain.Role.Hooks
{
    internal class ExcludeNavigationPropertiesHook : IBeforeCreate<Role>, IBeforeUpdate<Role>, IBeforeDelete<Role>
    {
        public Task ExecuteAsync(HookType type, IDomainEntityContext<Role> context, CancellationToken cancellationToken)
        {
            context.Entity.UserRoles = null;

            return Task.CompletedTask;
        }
    }
}