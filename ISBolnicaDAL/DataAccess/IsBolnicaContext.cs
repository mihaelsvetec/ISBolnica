using System;
using System.Collections.Generic;
using ISBolnicaDAL.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISBolnicaDAL.DataAccess;

public partial class IsBolnicaContext : DbContext
{
    public IsBolnicaContext()
    {
    }

    public IsBolnicaContext(DbContextOptions<IsBolnicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MedicinskiProizvod> MedicinskiProizvods { get; set; }

    public virtual DbSet<NezdravstveniRadnik> NezdravstveniRadniks { get; set; }

    public virtual DbSet<Odjel> Odjels { get; set; }

    public virtual DbSet<Pacijent> Pacijents { get; set; }

    public virtual DbSet<PovijestBolesti> PovijestBolestis { get; set; }

    public virtual DbSet<PovijestBolestiTerapija> PovijestBolestiTerapijas { get; set; }

    public virtual DbSet<Soba> Sobas { get; set; }

    public virtual DbSet<Terapija> Terapijas { get; set; }

    public virtual DbSet<TerminUredaj> TerminUredajs { get; set; }

    public virtual DbSet<Uredaj> Uredajs { get; set; }

    public virtual DbSet<Zaposlenik> Zaposleniks { get; set; }

    public virtual DbSet<ZdravstveniRadnik> ZdravstveniRadniks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQL2019;Database=isBolnica;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MedicinskiProizvod>(entity =>
        {
            entity.HasKey(e => e.MedicinskiProizvodId).HasName("PK__Medicins__5BCF24829CF9D5CD");

            entity.ToTable("MedicinskiProizvod");

            entity.Property(e => e.MedicinskiProizvodId).HasColumnName("medicinskiProizvodId");
            entity.Property(e => e.KategorijaProizvoda).HasColumnName("kategorijaProizvoda");
            entity.Property(e => e.KolicinaPakiranja)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("kolicinaPakiranja");
            entity.Property(e => e.MjernaJedinicaPakiranja)
                .HasMaxLength(16)
                .HasColumnName("mjernaJedinicaPakiranja");
            entity.Property(e => e.Naziv)
                .HasMaxLength(128)
                .HasColumnName("naziv");
            entity.Property(e => e.StanjeZaliha).HasColumnName("stanjeZaliha");
        });

        modelBuilder.Entity<NezdravstveniRadnik>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("NezdravstveniRadnik");

            entity.Property(e => e.OpisRadnogMjesta)
                .HasMaxLength(64)
                .HasColumnName("opisRadnogMjesta");
            entity.Property(e => e.RazinaOvlasti).HasColumnName("razinaOvlasti");
            entity.Property(e => e.ZaposlenikId).HasColumnName("zaposlenikId");

            entity.HasOne(d => d.Zaposlenik).WithMany()
                .HasForeignKey(d => d.ZaposlenikId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Nezdravst__zapos__31EC6D26");
        });

        modelBuilder.Entity<Odjel>(entity =>
        {
            entity.HasKey(e => e.OdjelId).HasName("PK__Odjel__7334E4D458323C70");

            entity.ToTable("Odjel");

            entity.Property(e => e.OdjelId).HasColumnName("odjelId");
            entity.Property(e => e.Ime)
                .HasMaxLength(64)
                .HasColumnName("ime");
            entity.Property(e => e.Lokacija)
                .HasMaxLength(128)
                .HasColumnName("lokacija");
            entity.Property(e => e.Opis)
                .HasMaxLength(256)
                .HasColumnName("opis");
        });

        modelBuilder.Entity<Pacijent>(entity =>
        {
            entity.HasKey(e => e.PacijentId).HasName("PK__Pacijent__79828489489ABA08");

            entity.ToTable("Pacijent");

            entity.HasIndex(e => e.Oib, "UQ__Pacijent__C2FFCF100E74726D").IsUnique();

            entity.Property(e => e.PacijentId).HasColumnName("pacijentId");
            entity.Property(e => e.Alergije)
                .HasMaxLength(256)
                .HasColumnName("alergije");
            entity.Property(e => e.DatumRodenja)
                .HasColumnType("date")
                .HasColumnName("datumRodenja");
            entity.Property(e => e.Ime)
                .HasMaxLength(32)
                .HasColumnName("ime");
            entity.Property(e => e.KrvnaGrupa)
                .HasMaxLength(3)
                .HasColumnName("krvnaGrupa");
            entity.Property(e => e.Oib)
                .HasMaxLength(11)
                .HasColumnName("oib");
            entity.Property(e => e.Prezime)
                .HasMaxLength(32)
                .HasColumnName("prezime");
            entity.Property(e => e.Spol)
                .HasMaxLength(1)
                .HasColumnName("spol");
        });

        modelBuilder.Entity<PovijestBolesti>(entity =>
        {
            entity.HasKey(e => e.PovijestBolestiId).HasName("PK__Povijest__F672FFBEC059B29D");

            entity.ToTable("PovijestBolesti");

            entity.Property(e => e.PovijestBolestiId).HasColumnName("povijestBolestiId");
            entity.Property(e => e.Dijagnoza).HasColumnName("dijagnoza");
            entity.Property(e => e.DoktorId).HasColumnName("doktorId");
            entity.Property(e => e.PacijentId).HasColumnName("pacijentId");
            entity.Property(e => e.Sazetak)
                .HasMaxLength(128)
                .HasColumnName("sazetak");
            entity.Property(e => e.Simptomi).HasColumnName("simptomi");
            entity.Property(e => e.SobaId).HasColumnName("sobaId");
            entity.Property(e => e.VrijemeOtvaranja)
                .HasColumnType("smalldatetime")
                .HasColumnName("vrijemeOtvaranja");
            entity.Property(e => e.VrijemeZatvaranja)
                .HasColumnType("smalldatetime")
                .HasColumnName("vrijemeZatvaranja");

            entity.HasOne(d => d.Doktor).WithMany(p => p.PovijestBolestis)
                .HasForeignKey(d => d.DoktorId)
                .HasConstraintName("FK__PovijestB__dokto__3A81B327");

            entity.HasOne(d => d.Pacijent).WithMany(p => p.PovijestBolestis)
                .HasForeignKey(d => d.PacijentId)
                .HasConstraintName("FK__PovijestB__pacij__398D8EEE");

            entity.HasOne(d => d.Soba).WithMany(p => p.PovijestBolestis)
                .HasForeignKey(d => d.SobaId)
                .HasConstraintName("FK__PovijestB__sobaI__3B75D760");
        });

        modelBuilder.Entity<PovijestBolestiTerapija>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PovijestBolestiTerapija");

            entity.Property(e => e.PovijestBolestiId).HasColumnName("povijestBolestiId");
            entity.Property(e => e.TerapijaId).HasColumnName("terapijaId");

            entity.HasOne(d => d.PovijestBolesti).WithMany()
                .HasForeignKey(d => d.PovijestBolestiId)
                .HasConstraintName("FK__PovijestB__povij__5BE2A6F2");

            entity.HasOne(d => d.Terapija).WithMany()
                .HasForeignKey(d => d.TerapijaId)
                .HasConstraintName("FK__PovijestB__terap__5CD6CB2B");
        });

        modelBuilder.Entity<Soba>(entity =>
        {
            entity.HasKey(e => e.SobaId).HasName("PK__Soba__469192B30A6D2F60");

            entity.ToTable("Soba");

            entity.Property(e => e.SobaId).HasColumnName("sobaId");
            entity.Property(e => e.Kapacitet).HasColumnName("kapacitet");
            entity.Property(e => e.KategorijaSobe).HasColumnName("kategorijaSobe");
            entity.Property(e => e.OdjelId).HasColumnName("odjelId");
            entity.Property(e => e.OznakaSobe)
                .HasMaxLength(16)
                .HasColumnName("oznakaSobe");

            entity.HasOne(d => d.Odjel).WithMany(p => p.Sobas)
                .HasForeignKey(d => d.OdjelId)
                .HasConstraintName("FK__Soba__odjelId__267ABA7A");
        });

        modelBuilder.Entity<Terapija>(entity =>
        {
            entity.HasKey(e => e.TerapijaId).HasName("PK__Terapija__A2208D40E7BBD69A");

            entity.ToTable("Terapija");

            entity.Property(e => e.TerapijaId).HasColumnName("terapijaId");
            entity.Property(e => e.OpisTerapije).HasColumnName("opisTerapije");
        });

        modelBuilder.Entity<TerminUredaj>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TerminUredaj");

            entity.Property(e => e.DatumVrijeme)
                .HasColumnType("smalldatetime")
                .HasColumnName("datumVrijeme");
            entity.Property(e => e.DoktorId).HasColumnName("doktorId");
            entity.Property(e => e.PacijentId).HasColumnName("pacijentId");
            entity.Property(e => e.UredajId).HasColumnName("uredajId");

            entity.HasOne(d => d.Doktor).WithMany()
                .HasForeignKey(d => d.DoktorId)
                .HasConstraintName("FK__TerminUre__dokto__3E52440B");

            entity.HasOne(d => d.Pacijent).WithMany()
                .HasForeignKey(d => d.PacijentId)
                .HasConstraintName("FK__TerminUre__pacij__3F466844");

            entity.HasOne(d => d.Uredaj).WithMany()
                .HasForeignKey(d => d.UredajId)
                .HasConstraintName("FK__TerminUre__ureda__3D5E1FD2");
        });

        modelBuilder.Entity<Uredaj>(entity =>
        {
            entity.HasKey(e => e.UredajId).HasName("PK__Uredaj__8BB69A9871B5F4BC");

            entity.ToTable("Uredaj");

            entity.Property(e => e.UredajId).HasColumnName("uredajId");
            entity.Property(e => e.Naziv)
                .HasMaxLength(128)
                .HasColumnName("naziv");
            entity.Property(e => e.OdjelId).HasColumnName("odjelId");
            entity.Property(e => e.SerijskiBroj)
                .HasMaxLength(128)
                .HasColumnName("serijskiBroj");
            entity.Property(e => e.SobaId).HasColumnName("sobaId");
            entity.Property(e => e.Upotreba)
                .HasMaxLength(256)
                .HasColumnName("upotreba");

            entity.HasOne(d => d.Odjel).WithMany(p => p.Uredajs)
                .HasForeignKey(d => d.OdjelId)
                .HasConstraintName("FK__Uredaj__odjelId__29572725");

            entity.HasOne(d => d.Soba).WithMany(p => p.Uredajs)
                .HasForeignKey(d => d.SobaId)
                .HasConstraintName("FK__Uredaj__sobaId__2A4B4B5E");
        });

        modelBuilder.Entity<Zaposlenik>(entity =>
        {
            entity.HasKey(e => e.ZaposlenikId).HasName("PK__Zaposlen__0BB66C78B6961DEC");

            entity.ToTable("Zaposlenik");

            entity.HasIndex(e => e.Oib, "UQ__Zaposlen__C2FFCF1064F5A7A4").IsUnique();

            entity.Property(e => e.ZaposlenikId).HasColumnName("zaposlenikId");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .HasColumnName("email");
            entity.Property(e => e.Ime)
                .HasMaxLength(32)
                .HasColumnName("ime");
            entity.Property(e => e.OdjelId).HasColumnName("odjelId");
            entity.Property(e => e.Oib)
                .HasMaxLength(11)
                .HasColumnName("oib");
            entity.Property(e => e.Prezime)
                .HasMaxLength(32)
                .HasColumnName("prezime");
            entity.Property(e => e.ZdravstevniRadnik).HasColumnName("zdravstevniRadnik");

            entity.HasOne(d => d.Odjel).WithMany(p => p.Zaposleniks)
                .HasForeignKey(d => d.OdjelId)
                .HasConstraintName("FK__Zaposleni__odjel__2E1BDC42");
        });

        modelBuilder.Entity<ZdravstveniRadnik>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ZdravstveniRadnik");

            entity.Property(e => e.OpisRadnogMjesta)
                .HasMaxLength(64)
                .HasColumnName("opisRadnogMjesta");
            entity.Property(e => e.RadnoMjesto).HasColumnName("radnoMjesto");
            entity.Property(e => e.ZaposlenikId).HasColumnName("zaposlenikId");

            entity.HasOne(d => d.Zaposlenik).WithMany()
                .HasForeignKey(d => d.ZaposlenikId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Zdravstve__zapos__300424B4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
