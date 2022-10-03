namespace Journal.Migration.Migrations;

[Migration(00002)]
public class CreateIdentityTables_00002 : FluentMigrator.Migration
{
    private const string DB_SCHEMA = Journal.Identity.Constants.IDENTITY_DB_SCHEMA;

    public override void Up()
    {
        InternalCreateIdentityFrameworkTables();
        InternalCreateRefreshTokenTable();
    }

    private void InternalCreateIdentityFrameworkTables()
    {
        Create.Table("role").InSchema(DB_SCHEMA)
            .WithColumn("id").AsString().PrimaryKey()
            .WithColumn("concurrency_stamp").AsString().Nullable()
            .WithColumn("name").AsString(256).NotNullable()
            .WithColumn("normalized_name").AsString(256).Nullable()
                .Indexed("role_name_index");

        Create.Table("app_user_tokens").InSchema(DB_SCHEMA)
            .WithColumn("user_id").AsString().PrimaryKey().NotNullable()
            .WithColumn("login_provider").AsString()
            .WithColumn("name").AsString()
            .WithColumn("value").AsString();

        Create.Table("app_user").InSchema(DB_SCHEMA)
           .WithColumn("id").AsString().NotNullable().PrimaryKey()
           .WithColumn("secondary_id").AsInt32().Identity()
           .WithColumn("access_failed_count").AsInt32().NotNullable()
           .WithColumn("concurrency_stamp").AsString().Nullable()
           .WithColumn("email").AsString(256).Nullable()
           .WithColumn("email_confirmed").AsBoolean().NotNullable()
           .WithColumn("lockout_enabled").AsBoolean().NotNullable()
           .WithColumn("lockout_end").AsDateTimeOffset().Nullable()
           .WithColumn("normalized_email").AsString().Nullable()
           .WithColumn("normalized_user_name").AsString().Nullable()
           .WithColumn("password_hash").AsString().Nullable()
           .WithColumn("phone_number").AsString().Nullable()
           .WithColumn("phone_number_confirmed").AsBoolean().NotNullable()
           .WithColumn("security_stamp").AsString().Nullable()
           .WithColumn("two_factor_enabled").AsBoolean().NotNullable()
           .WithColumn("user_name").AsString(256).Nullable();

        Create.Table("role_claims").InSchema(DB_SCHEMA)
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("claim_type").AsString().Nullable()
            .WithColumn("claim_value").AsString().Nullable()
            .WithColumn("role_id").AsString().NotNullable().Indexed("idx_asp_net_role_claims_role_id");

        Create.ForeignKey()
            .FromTable("role_claims").InSchema(DB_SCHEMA).ForeignColumn("role_id")
            .ToTable("role").InSchema(DB_SCHEMA).PrimaryColumn("id");

        Create.Table("app_user_claims").InSchema(DB_SCHEMA)
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("claim_type").AsString().Nullable()
            .WithColumn("claim_value").AsString().Nullable()
            .WithColumn("user_id").AsString().NotNullable().Indexed("idx_asp_net_user_claims_user_id");

        Create.ForeignKey()
            .FromTable("app_user_claims").InSchema(DB_SCHEMA).ForeignColumn("user_id")
            .ToTable("app_user").InSchema(DB_SCHEMA).PrimaryColumn("id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.Table("app_user_logins").InSchema(DB_SCHEMA)
            .WithColumn("login_provider").AsString().NotNullable().PrimaryKey()
            .WithColumn("provider_key").AsString().NotNullable().PrimaryKey()
            .WithColumn("provider_display_name").AsString().Nullable()
            .WithColumn("user_id").AsString()
            .NotNullable()
            .Indexed("idx_asp_net_user_logins_user_id");

        Create.ForeignKey()
            .FromTable("app_user_logins").InSchema(DB_SCHEMA).ForeignColumn("user_id")
            .ToTable("app_user").InSchema(DB_SCHEMA).PrimaryColumn("id")
            .OnDelete(System.Data.Rule.Cascade);


        Create.Table("app_user_roles").InSchema(DB_SCHEMA)
            .WithColumn("user_id").AsString().PrimaryKey()
                .Indexed("idx_asp_net_user_roles_user_id")

            .WithColumn("role_id").AsString().PrimaryKey()
                .Indexed("ix_asp_net_user_roles_role_id");

        Create.ForeignKey()
            .FromTable("app_user_roles").InSchema(DB_SCHEMA).ForeignColumn("user_id")
            .ToTable("app_user").InSchema(DB_SCHEMA).PrimaryColumn("id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.ForeignKey()
            .FromTable("app_user_roles").InSchema(DB_SCHEMA).ForeignColumn("role_id")
            .ToTable("role").InSchema(DB_SCHEMA).PrimaryColumn("id");
    }

    private void InternalCreateRefreshTokenTable()
    {
        Create.Table("refresh_token").InSchema(DB_SCHEMA)
            .WithColumn("token").AsString().PrimaryKey()
            .WithColumn("jwt_id").AsString()
            .WithColumn("creation_date").AsDateTimeOffset()
            .WithColumn("expiration_date").AsDateTimeOffset()
            .WithColumn("used").AsBoolean()
            .WithColumn("invalidated").AsBoolean()
            .WithColumn("user_id").AsString();

        Create.ForeignKey()
            .FromTable("refresh_token").InSchema(DB_SCHEMA).ForeignColumn("user_id")
            .ToTable("app_user").InSchema(DB_SCHEMA).PrimaryColumn("id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.Index("idx_refresh_token_used")
            .OnTable("refresh_token").InSchema(DB_SCHEMA)
            .WithOptions().NonClustered()
            .OnColumn("used");

        Create.Index("idx_refresh_token_invalidated")
            .OnTable("refresh_token").InSchema(DB_SCHEMA)
            .WithOptions().NonClustered()
            .OnColumn("invalidated");
    }

    public override void Down()
    {
        Delete.Table("refresh_token").InSchema(DB_SCHEMA);
        Delete.Table("app_user_roles").InSchema(DB_SCHEMA);
        Delete.Table("app_user_logins").InSchema(DB_SCHEMA);
        Delete.Table("app_user_claims").InSchema(DB_SCHEMA);
        Delete.Table("role_claims").InSchema(DB_SCHEMA);
        Delete.Table("app_user").InSchema(DB_SCHEMA);
        Delete.Table("app_user_tokens").InSchema(DB_SCHEMA);
        Delete.Table("role").InSchema(DB_SCHEMA);
    }
}