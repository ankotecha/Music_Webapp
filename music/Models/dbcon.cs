namespace music.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class dbcon : DbContext
    {
        // Your context has been configured to use a 'dbcon' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'music.Models.dbcon' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'dbcon' 
        // connection string in the application configuration file.
        public dbcon()
            : base("name=DefaultConnection")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<Song> Song { get; set; }
        public DbSet<UserPlaylist> userplaylists { get; set; }
        public DbSet<Playlists> playlists { get; set; }
        public virtual DbSet<FavouriteSong> favouritesong { get; set; }

        public virtual DbSet<SongHistory> sh { get; set; }

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}