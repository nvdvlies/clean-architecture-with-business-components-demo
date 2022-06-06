﻿using System.Collections.Generic;
using Demo.Common.Interfaces;
using Demo.Domain.Auditlog;
using Demo.Domain.Auditlog.Interfaces;
using Demo.Domain.Role;
using Demo.Domain.Shared.Interfaces;
using Demo.Infrastructure.Auditlogging.Shared;

namespace Demo.Infrastructure.Auditlogging
{
    internal class RoleAuditlogger : AuditloggerBase<Role>, IAuditlogger<Role>
    {
        public RoleAuditlogger(
            ICurrentUser currentUser,
            IDateTime dateTime,
            IAuditlogDomainEntity auditlogDomainEntity
        ) : base(currentUser, dateTime, auditlogDomainEntity)
        {
        }

        protected override List<AuditlogItem> AuditlogItems(Role current, Role previous) =>
            new AuditlogBuilder<Role>()
                .WithProperty(x => x.Name)
                .WithProperty(x => x.ExternalId)
                .Build(current, previous);
    }
}