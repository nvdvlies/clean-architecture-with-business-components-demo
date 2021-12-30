﻿using Demo.Common.Interfaces;
using Demo.Domain.ApplicationSettings.Interfaces;
using Demo.Domain.Shared.DomainEntity;
using Demo.Domain.Shared.Exceptions;
using Demo.Domain.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Domain.ApplicationSettings
{
    internal class ApplicationSettingsDomainEntity : DomainEntity<ApplicationSettings>, IApplicationSettingsDomainEntity
    {
        private readonly IDbCommandForTableWithSingleRecord<ApplicationSettings> _dbCommand;

        public ApplicationSettingsDomainEntity(
            ILogger<ApplicationSettingsDomainEntity> logger,
            ICurrentUser currentUser,
            IDateTime dateTime,
            IDbCommandForTableWithSingleRecord<ApplicationSettings> dbCommand,
            IEnumerable<IDefaultValuesSetter<ApplicationSettings>> defaultValuesSetters,
            IEnumerable<IValidator<ApplicationSettings>> validators,
            IEnumerable<IBeforeCreate<ApplicationSettings>> beforeCreateHooks,
            IEnumerable<IAfterCreate<ApplicationSettings>> afterCreateHooks,
            IEnumerable<IBeforeUpdate<ApplicationSettings>> beforeUpdateHooks,
            IEnumerable<IAfterUpdate<ApplicationSettings>> afterUpdateHooks,
            IEnumerable<IBeforeDelete<ApplicationSettings>> beforeDeleteHooks,
            IEnumerable<IAfterDelete<ApplicationSettings>> afterDeleteHooks,
            IEventOutboxProcessor eventOutboxProcessor,
            IMessageOutboxProcessor messageOutboxProcessor,
            IJsonService<ApplicationSettings> jsonService,
            IAuditlogger<ApplicationSettings> auditlogger
        )
            : base(logger, currentUser, dateTime, dbCommand, defaultValuesSetters, validators, beforeCreateHooks, afterCreateHooks, beforeUpdateHooks, afterUpdateHooks, beforeDeleteHooks, afterDeleteHooks, eventOutboxProcessor, messageOutboxProcessor, jsonService, auditlogger)
        {
            _dbCommand = dbCommand;
        }

        public async Task GetAsync(CancellationToken cancellationToken = default)
        {
            var entity = await _dbCommand.GetAsync(cancellationToken);
            if (entity == null)
            {
                await NewAsync(cancellationToken);
                return;
            }
            Context.Entity = entity;
        }

        public override Task DeleteAsync(CancellationToken cancellationToken = default)
        {
            throw new DomainException($"{nameof(ApplicationSettings)} cannot be deleted.");
        }
    }
}
