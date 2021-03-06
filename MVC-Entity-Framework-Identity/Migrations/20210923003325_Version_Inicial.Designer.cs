// <auto-generated />
using System;
using MVC_Entity_Framework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVC_Entity_Framework.Migrations
{
    [DbContext(typeof(MVC_Entity_FrameworkContext))]
    [Migration("20210923003325_Version_Inicial")]
    partial class Version_Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15");

            modelBuilder.Entity("MVC_Entity_Framework.Models.Calificacion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EstudianteId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MateriaId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Valor")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("MateriaId");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("MVC_Entity_Framework.Models.Contacto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Celular")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EstudianteId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Facebook")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("Instagram")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Twitter")
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId")
                        .IsUnique();

                    b.ToTable("Contactos");
                });

            modelBuilder.Entity("MVC_Entity_Framework.Models.Estudiante", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(80);

                    b.Property<int>("Dni")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaDeNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("MVC_Entity_Framework.Models.Materia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("MVC_Entity_Framework.Models.MateriaEstudiante", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EstudianteId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MateriaId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("MateriaId");

                    b.ToTable("MateriasEstudiantes");
                });

            modelBuilder.Entity("MVC_Entity_Framework.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Contraseña")
                        .HasColumnType("BLOB");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("Rol")
                        .HasColumnType("INTEGER");

                    b.Property<string>("User")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MVC_Entity_Framework.Models.Calificacion", b =>
                {
                    b.HasOne("MVC_Entity_Framework.Models.Estudiante", "Estudiante")
                        .WithMany("Calificaciones")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVC_Entity_Framework.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVC_Entity_Framework.Models.Contacto", b =>
                {
                    b.HasOne("MVC_Entity_Framework.Models.Estudiante", "Estudiante")
                        .WithOne("Contacto")
                        .HasForeignKey("MVC_Entity_Framework.Models.Contacto", "EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVC_Entity_Framework.Models.MateriaEstudiante", b =>
                {
                    b.HasOne("MVC_Entity_Framework.Models.Estudiante", "Estudiante")
                        .WithMany("Materias")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVC_Entity_Framework.Models.Materia", "Materia")
                        .WithMany("Estudiantes")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
