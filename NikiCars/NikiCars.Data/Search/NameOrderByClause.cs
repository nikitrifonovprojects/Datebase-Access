﻿using NikiCars.Search;

namespace NikiCars.Data.Search
{
    public class NameOrderByClause : OrderBy
    {
        private OrderByEnum orderBy;

        public NameOrderByClause(OrderByEnum orderBy)
        {
            this.orderBy = orderBy;
        }

        public override string GenerateOrderByClause()
        {
            if (this.orderBy == OrderByEnum.None)
            {
                return "Name";
            }
            else if (this.orderBy == OrderByEnum.Ascending)
            {
                return "Name ASC";
            }
            else
            {
                return "Name DESC";
            }
        }
    }
}
