﻿using Microsoft.EntityFrameworkCore;
using Proyecto_Software_Individual.Domain.Models;

namespace Proyecto_Software_Individual.Infraestructure.Data.Seeds
{
    public static class ApprovalRuleSeeds
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApprovalRule>().HasData(
            new ApprovalRule { Id = 1, MinAmount = 0, MaxAmount = 10000, AreaId = null, Type = null, StepOrder = 1, ApproverRoleId = 1 },
            new ApprovalRule { Id = 2, MinAmount = 5000, MaxAmount = 20000, AreaId = null, Type = null, StepOrder = 2, ApproverRoleId = 2 },
            new ApprovalRule { Id = 3, MinAmount = 0, MaxAmount = 20000, AreaId = 2, Type = 2, StepOrder = 1, ApproverRoleId = 2 },
            new ApprovalRule { Id = 4, MinAmount = 20000, MaxAmount = 0, AreaId = null, Type = null, StepOrder = 3, ApproverRoleId = 3 },
            new ApprovalRule { Id = 5, MinAmount = 5000, MaxAmount = 0, AreaId = 1, Type = 1, StepOrder = 2, ApproverRoleId = 2 },
            new ApprovalRule { Id = 6, MinAmount = 0, MaxAmount = 10000, AreaId = null, Type = 2, StepOrder = 1, ApproverRoleId = 1 },
            new ApprovalRule { Id = 7, MinAmount = 0, MaxAmount = 10000, AreaId = 2, Type = 1, StepOrder = 1, ApproverRoleId = 4 },
            new ApprovalRule { Id = 8, MinAmount = 10000, MaxAmount = 30000, AreaId = 2, Type = null, StepOrder = 2, ApproverRoleId = 2 },
            new ApprovalRule { Id = 9, MinAmount = 30000, MaxAmount = 0, AreaId = 3, Type = null, StepOrder = 2, ApproverRoleId = 3 },
            new ApprovalRule { Id = 10, MinAmount = 0, MaxAmount = 50000, AreaId = null, Type = 4, StepOrder = 1, ApproverRoleId = 4 }
            );
        }
    }
}
