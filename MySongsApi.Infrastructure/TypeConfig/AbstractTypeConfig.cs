using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongsApi.Infrastructure.TypeConfig
{
    public abstract class AbstractTypeConfig<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        public AbstractTypeConfig()
        {
            ConfigTableName();
            ConfigTableFields();
            ConfigPrimaryKey();
            ConfigForeignKey();
        }

        protected abstract void ConfigTableName();
        protected abstract void ConfigTableFields();
        protected abstract void ConfigPrimaryKey();
        protected abstract void ConfigForeignKey();
    }
}
