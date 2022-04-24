using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ServiceEventEF.Models
{
    public partial class DB_9AE8B0_GeventDlloContext : DbContext
    {
        public DB_9AE8B0_GeventDlloContext()
        {
        }

        public DB_9AE8B0_GeventDlloContext(DbContextOptions<DB_9AE8B0_GeventDlloContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<ActividadCategoria> ActividadCategoria { get; set; }
        public virtual DbSet<AsignaEvento> AsignaEvento { get; set; }
        public virtual DbSet<Asistencia> Asistencia { get; set; }
        public virtual DbSet<CodigoQr> CodigoQr { get; set; }
        public virtual DbSet<Contacto> Contacto { get; set; }
        public virtual DbSet<CuestionarioActividad> CuestionarioActividad { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<FormaPago> FormaPago { get; set; }
        public virtual DbSet<Inscripcion> Inscripcion { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<MaterialEntrega> MaterialEntrega { get; set; }
        public virtual DbSet<MenuMaster> MenuMaster { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<Registro> Registro { get; set; }
        public virtual DbSet<Tarifa> Tarifa { get; set; }
        public virtual DbSet<TriviaOpcion> TriviaOpcion { get; set; }
        public virtual DbSet<TriviaPregunta> TriviaPregunta { get; set; }
        public virtual DbSet<TriviaRespuesta> TriviaRespuesta { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=sql5026.site4now.net;Initial Catalog=DB_9AE8B0_GeventDllo;Persist Security Info=True;User ID=DB_9AE8B0_GeventDllo_admin; user=DB_9AE8B0_GeventDllo_admin;password=Gevent2018;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.IdActividad);

                entity.Property(e => e.IdActividad).HasColumnName("Id_Actividad");

                entity.Property(e => e.Ciudad).HasMaxLength(20);

                entity.Property(e => e.CupoMaximoInscripciones).HasColumnName("Cupo_Maximo_Inscripciones");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion).HasMaxLength(50);

                entity.Property(e => e.FechaFin)
                    .HasColumnName("Fecha_Fin")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("Fecha_Inicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.HoraFin)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoraInicio)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");

                entity.Property(e => e.Lugar).HasMaxLength(30);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Pais).HasMaxLength(30);

                entity.HasOne(d => d.ConferencistaNavigation)
                    .WithMany(p => p.ActividadConferencistaNavigation)
                    .HasForeignKey(d => d.Conferencista)
                    .HasConstraintName("FK_Contacto1Actividad");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Actividad)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Parametros1Actividad");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Actividad)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento1Actividad");

                entity.HasOne(d => d.ModeradorNavigation)
                    .WithMany(p => p.ActividadModeradorNavigation)
                    .HasForeignKey(d => d.Moderador)
                    .HasConstraintName("FK_Contacto2Actividad");
            });

            modelBuilder.Entity<ActividadCategoria>(entity =>
            {
                entity.HasKey(e => e.IdActividadCategoria);

                entity.ToTable("Actividad_Categoria");

                entity.Property(e => e.IdActividadCategoria).HasColumnName("Id_Actividad_Categoria");

                entity.Property(e => e.IdActividad).HasColumnName("Id_Actividad");

                entity.Property(e => e.IdCategoria).HasColumnName("Id_Categoria");

                entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.ActividadCategoria)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actividad_Categoria1Actividad");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.ActividadCategoria)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actividad_Categoria1Parametro");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.ActividadCategoria)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actividad_Categoria1Evento");
            });

            modelBuilder.Entity<AsignaEvento>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaRelacion).HasColumnType("datetime");

                entity.Property(e => e.IdEvento).HasColumnName("idEvento");
            });

            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.HasKey(e => e.IdAsistencia);

                entity.Property(e => e.IdAsistencia).HasColumnName("Id_Asistencia");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.FechaHoraIngreso)
                    .HasColumnName("Fecha_Hora_Ingreso")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaHoraSalida)
                    .HasColumnName("Fecha_Hora_Salida")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdActividad).HasColumnName("Id_Actividad");

                entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");

                entity.Property(e => e.IdRegistro).HasColumnName("Id_Registro");

                entity.Property(e => e.TiempoParticipacion).HasColumnName("Tiempo_Participacion");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actividad1Asistencia");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento1Asistencia");

                entity.HasOne(d => d.IdRegistroNavigation)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.IdRegistro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registro1Asistencia");
            });

            modelBuilder.Entity<CodigoQr>(entity =>
            {
                entity.HasKey(e => e.IdCodigoQr);

                entity.ToTable("Codigo_QR");

                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ__Codigo_Q__75E3EFCF06B4F7EE")
                    .IsUnique();

                entity.Property(e => e.IdCodigoQr).HasColumnName("Id_Codigo_QR");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.Nombre).HasMaxLength(300);
            });

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.IdContacto);

                entity.Property(e => e.IdContacto).HasColumnName("Id_Contacto");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Cargo).HasMaxLength(50);

                entity.Property(e => e.CorreoElectronico)
                    .HasColumnName("Correo_Electronico")
                    .HasMaxLength(50);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentoIdentidad)
                    .HasColumnName("Documento_Identidad")
                    .HasMaxLength(15);

                entity.Property(e => e.EmpresaNombre)
                    .HasColumnName("Empresa_Nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.IdEmpresa).HasColumnName("id_Empresa");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Nombres).HasMaxLength(50);

                entity.Property(e => e.Observacion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Pais).HasMaxLength(30);

                entity.Property(e => e.Profesion).HasMaxLength(50);

                entity.Property(e => e.TelefonoFijo)
                    .HasColumnName("Telefono_fijo")
                    .HasMaxLength(20);

                entity.Property(e => e.TelefonoMovil)
                    .HasColumnName("Telefono_movil")
                    .HasMaxLength(20);

                entity.HasOne(d => d.CategoriaNavigation)
                    .WithMany(p => p.ContactoCategoriaNavigation)
                    .HasForeignKey(d => d.Categoria)
                    .HasConstraintName("FK_Parametro1Contacto");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.ContactoEstadoNavigation)
                    .HasForeignKey(d => d.Estado)
                    .HasConstraintName("FK_Parametro2Contacto");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Contacto)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK__Contacto__Estado__182C9B23");
            });

            modelBuilder.Entity<CuestionarioActividad>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("PK_CuestionarioActividad")
                    .IsUnique();

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.IdActividad).HasColumnName("Id_Actividad");

                entity.Property(e => e.IdInscritos).HasColumnName("Id_Inscritos");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.Property(e => e.IdEmpresa).HasColumnName("Id_Empresa");

                entity.Property(e => e.CorreoElectronico)
                    .HasColumnName("Correo_Electronico")
                    .HasMaxLength(30);

                entity.Property(e => e.CupoMaximoAsistentes).HasColumnName("Cupo_Maximo_Asistentes");

                entity.Property(e => e.Direccion).HasMaxLength(50);

                entity.Property(e => e.Nit)
                    .IsRequired()
                    .HasColumnName("NIT")
                    .HasMaxLength(15);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pais).HasMaxLength(30);

                entity.Property(e => e.TelefonoFijo)
                    .HasColumnName("Telefono_fijo")
                    .HasMaxLength(20);

                entity.Property(e => e.TelefonoMovil)
                    .HasColumnName("Telefono_movil")
                    .HasMaxLength(20);

                entity.Property(e => e.TipoIdentificacion)
                    .HasColumnName("Tipo_Identificacion")
                    .HasMaxLength(30);

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Empresa)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Empresa__Estado__15502E78");
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.IdEvento);

                entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");

                entity.Property(e => e.Ciudad).HasMaxLength(20);

                entity.Property(e => e.CupoMaximoInscripciones).HasColumnName("Cupo_Maximo_Inscripciones");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion).HasMaxLength(50);

                entity.Property(e => e.FechaFin)
                    .HasColumnName("Fecha_Fin")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("Fecha_Inicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.HoraFin)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoraInicio)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Latitud).HasColumnType("numeric(18, 7)");

                entity.Property(e => e.Longitud).HasColumnType("numeric(18, 7)");

                entity.Property(e => e.Lugar).HasMaxLength(30);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Pais).HasMaxLength(30);

                entity.Property(e => e.RutaImagen).HasMaxLength(250);

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Evento)
                    .HasForeignKey(d => d.Estado)
                    .HasConstraintName("FK_Parametro1Evento");
            });

            modelBuilder.Entity<FormaPago>(entity =>
            {
                entity.HasKey(e => e.IdFormaPago);

                entity.ToTable("Forma_Pago");

                entity.Property(e => e.IdFormaPago).HasColumnName("Id_Forma_Pago");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(20);
            });

            modelBuilder.Entity<Inscripcion>(entity =>
            {
                entity.HasKey(e => e.IdInscripcion);

                entity.Property(e => e.IdInscripcion).HasColumnName("Id_Inscripcion");

                entity.Property(e => e.Acompanante).HasMaxLength(50);

                entity.Property(e => e.EmpresaPatrocinadora)
                    .HasColumnName("Empresa_Patrocinadora")
                    .HasMaxLength(30);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdActividad).HasColumnName("Id_Actividad");

                entity.Property(e => e.IdContacto).HasColumnName("Id_Contacto");

                entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");

                entity.Property(e => e.IdPago).HasColumnName("Id_Pago");

                entity.Property(e => e.Motivo).HasMaxLength(100);

                entity.Property(e => e.Visitante).HasMaxLength(50);

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Inscripcion)
                    .HasForeignKey(d => d.Estado)
                    .HasConstraintName("FK_Parametros1Inscripcion");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Inscripcion)
                    .HasForeignKey(d => d.IdActividad)
                    .HasConstraintName("FK_Parametros2Inscripcion");

                entity.HasOne(d => d.IdContactoNavigation)
                    .WithMany(p => p.Inscripcion)
                    .HasForeignKey(d => d.IdContacto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacto1Inscripcion");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Inscripcion)
                    .HasForeignKey(d => d.IdEvento)
                    .HasConstraintName("FK_Evento1Inscripcion");

                entity.HasOne(d => d.IdPagoNavigation)
                    .WithMany(p => p.Inscripcion)
                    .HasForeignKey(d => d.IdPago)
                    .HasConstraintName("FK_Pagos1Inscripcion");
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.HasKey(e => e.IdLogRegistro);

                entity.Property(e => e.IdLogRegistro).HasColumnName("Id_Log_Registro");

                entity.Property(e => e.DescripcionActividad)
                    .HasColumnName("Descripcion_Actividad")
                    .HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IpMaquina)
                    .HasColumnName("IP_Maquina")
                    .HasMaxLength(20);

                entity.Property(e => e.NombreMaquina)
                    .HasColumnName("Nombre_Maquina")
                    .HasMaxLength(50);

                entity.Property(e => e.TablaAfectada)
                    .HasColumnName("Tabla_Afectada")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.IdActividad).HasColumnName("Id_Actividad");

                entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");

                entity.Property(e => e.IdMaterial).HasColumnName("Id_Material");

                entity.HasOne(d => d.IdMaterialNavigation)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.IdMaterial)
                    .HasConstraintName("FK_Parametro_Material");
            });

            modelBuilder.Entity<MaterialEntrega>(entity =>
            {
                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdInscrito).HasColumnName("Id_Inscrito");

                entity.Property(e => e.IdMaterial).HasColumnName("Id_Material");
            });

            modelBuilder.Entity<MenuMaster>(entity =>
            {
                entity.HasKey(e => new { e.MenuIdentity, e.MenuId, e.MenuName });

                entity.Property(e => e.MenuIdentity).ValueGeneratedOnAdd();

                entity.Property(e => e.MenuId)
                    .HasColumnName("MenuID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.LogoMenu)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.MenuFileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MenuUrl)
                    .IsRequired()
                    .HasColumnName("MenuURL")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ParentMenuId)
                    .IsRequired()
                    .HasColumnName("Parent_MenuID")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UseYn)
                    .HasColumnName("USE_YN")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Y')");

                entity.Property(e => e.UserRoll)
                    .IsRequired()
                    .HasColumnName("User_Roll")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago);

                entity.Property(e => e.IdPago).HasColumnName("Id_Pago");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.FechaConformacion)
                    .HasColumnName("Fecha_Conformacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaSolicitud)
                    .HasColumnName("Fecha_Solicitud")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdContacto).HasColumnName("Id_Contacto");

                entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");

                entity.Property(e => e.IdFormaPago).HasColumnName("Id_Forma_Pago");

                entity.Property(e => e.MontoMonetario)
                    .HasColumnName("Monto_Monetario")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.PagoEstadoNavigation)
                    .HasForeignKey(d => d.Estado)
                    .HasConstraintName("Parametros2Pago");

                entity.HasOne(d => d.IdFormaPagoNavigation)
                    .WithMany(p => p.Pago)
                    .HasForeignKey(d => d.IdFormaPago)
                    .HasConstraintName("FK_Forma_Pago1Pago");

                entity.HasOne(d => d.MonedaNavigation)
                    .WithMany(p => p.PagoMonedaNavigation)
                    .HasForeignKey(d => d.Moneda)
                    .HasConstraintName("Parametros1Pago");
            });

            modelBuilder.Entity<Parametros>(entity =>
            {
                entity.HasKey(e => e.IdParametro);

                entity.Property(e => e.IdParametro).HasColumnName("Id_Parametro");

                entity.Property(e => e.Descripcion).HasMaxLength(500);

                entity.Property(e => e.IdTipo).HasColumnName("id_Tipo");

                entity.Property(e => e.NombreParametro)
                    .IsRequired()
                    .HasColumnName("Nombre_Parametro")
                    .HasMaxLength(100);

                entity.Property(e => e.NombreTipo)
                    .IsRequired()
                    .HasColumnName("Nombre_Tipo")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.BuyerEmail)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmationUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.Currency)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Error)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ResponseUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippingCountry)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Signature).HasMaxLength(32);

                entity.Property(e => e.Tax).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.TaxReturnBase).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_at")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Registro>(entity =>
            {
                entity.HasKey(e => e.IdRegistro);

                entity.Property(e => e.IdRegistro).HasColumnName("Id_Registro");

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdActividad).HasColumnName("Id_Actividad");

                entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");

                entity.Property(e => e.IdInscripcion).HasColumnName("Id_Inscripcion");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Registro)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actividad1Registro");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Registro)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento1Registro");

                entity.HasOne(d => d.IdInscripcionNavigation)
                    .WithMany(p => p.Registro)
                    .HasForeignKey(d => d.IdInscripcion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inscripcion1Registro");
            });

            modelBuilder.Entity<Tarifa>(entity =>
            {
                entity.HasKey(e => e.IdTarifa);

                entity.Property(e => e.IdTarifa).HasColumnName("Id_Tarifa");

                entity.Property(e => e.IdActividad).HasColumnName("Id_Actividad");

                entity.Property(e => e.IdEvento).HasColumnName("Id_Evento");

                entity.Property(e => e.MontoMonetario)
                    .HasColumnName("Monto_Monetario")
                    .HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Tarifa)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actividad1Tarifa");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Tarifa)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evento1Tarifa");

                entity.HasOne(d => d.MonedaNavigation)
                    .WithMany(p => p.Tarifa)
                    .HasForeignKey(d => d.Moneda)
                    .HasConstraintName("FK_Parametro1Tarifa");
            });

            modelBuilder.Entity<TriviaOpcion>(entity =>
            {
                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.IdPregunta).HasColumnName("Id_Pregunta");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPreguntaNavigation)
                    .WithMany(p => p.TriviaOpcion)
                    .HasForeignKey(d => d.IdPregunta)
                    .HasConstraintName("FK_Pregunta_Opciones");
            });

            modelBuilder.Entity<TriviaPregunta>(entity =>
            {
                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.IdCuestionario).HasColumnName("Id_Cuestionario");

                entity.Property(e => e.Indicio)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RutaImagen)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TriviaRespuesta>(entity =>
            {
                entity.Property(e => e.FechaCrecion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.IdActividad).HasColumnName("Id_Actividad");

                entity.Property(e => e.IdContacto).HasColumnName("Id_Contacto");

                entity.Property(e => e.IdOpciones).HasColumnName("Id_Opciones");

                entity.Property(e => e.IdPreguntas).HasColumnName("Id_Preguntas");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.TriviaRespuesta)
                    .HasForeignKey(d => d.IdActividad)
                    .HasConstraintName("FK_Actividad_Respuesta");

                entity.HasOne(d => d.IdContactoNavigation)
                    .WithMany(p => p.TriviaRespuesta)
                    .HasForeignKey(d => d.IdContacto)
                    .HasConstraintName("FK_Contacto_Respuesta");

                entity.HasOne(d => d.IdOpcionesNavigation)
                    .WithMany(p => p.TriviaRespuesta)
                    .HasForeignKey(d => d.IdOpciones)
                    .HasConstraintName("FK_Opciones_Respuesta");

                entity.HasOne(d => d.IdPreguntasNavigation)
                    .WithMany(p => p.TriviaRespuesta)
                    .HasForeignKey(d => d.IdPreguntas)
                    .HasConstraintName("FK_Respuesta_Pregunta");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Login });

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.ConcesionariaId).HasColumnName("concesionaria_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.CryptedPassword)
                    .IsRequired()
                    .HasColumnName("crypted_password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentLoginAt)
                    .HasColumnName("current_login_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.CurrentLoginIp)
                    .HasColumnName("current_login_ip")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.InReportMail).HasColumnName("in_report_mail");

                entity.Property(e => e.LastLoginAt)
                    .HasColumnName("last_login_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastLoginIp)
                    .HasColumnName("last_login_ip")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastRequestAt)
                    .HasColumnName("last_request_at")
                    .HasColumnType("datetime");

                entity.Property(e => e.LoginCount).HasColumnName("login_count");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("password_salt")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PersistenceToken)
                    .IsRequired()
                    .HasColumnName("persistence_token")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SingleAccessToken)
                    .IsRequired()
                    .HasColumnName("single_access_token")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('active')");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("datetime");
            });
        }
    }
}
