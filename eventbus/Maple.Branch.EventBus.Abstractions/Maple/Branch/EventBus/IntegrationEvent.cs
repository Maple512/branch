// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Text.Json.Serialization;
using MapleClub.Utility;

namespace Maple.Branch.EventBus
{
    public abstract class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = GuidHelper.Generator();
            CreatedAt = DateTimeOffset.Now;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTimeOffset createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }

        public Guid Id { get; }

        public DateTimeOffset CreatedAt { get; }

        public virtual string GetEventName() => string.Empty;
    }
}
