namespace DAL
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class BlackJackContext : DbContext
	{
		public BlackJackContext()
			: base("name=BlackJackContext")
		{
		}

		public virtual DbSet<PlayLog> PlayLog { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PlayLog>()
				.Property(e => e.SessionID)
				.IsUnicode(false);
		}
	}
}
