using FluentMigrator;

namespace mode_api.data
{
    [Migration(1)]
    public class CreateTables : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("mode_detail")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("external_id").AsGuid().NotNullable().Unique()
                .WithColumn("name_platonic").AsString().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_date").AsDateTime().NotNullable()
                .WithColumn("modified_by").AsInt32().Nullable()
                .WithColumn("modified_date").AsDateTime().Nullable()
                .WithColumn("version").AsInt64().NotNullable();
        }
    }
}
