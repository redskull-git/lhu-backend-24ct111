using LHU_MaSV_NguyenVanTi.Api.Database.Entities;
using LHU_MaSV_NguyenVanTi.Api.Database.Seeds;
using Microsoft.EntityFrameworkCore;

namespace LHU_MaSV_NguyenVanTi.Api.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<MonHocEntity> MonHoc => Set<MonHocEntity>();

        public DbSet<LopHocPhanEntity> LopHocPhan => Set<LopHocPhanEntity>();

        public DbSet<SinhVienEntity> SinhViens => Set<SinhVienEntity>();

        public DbSet<DangKyEntity> DangKies => Set<DangKyEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình seed data từ thư mục Seeds
            modelBuilder.ApplyConfiguration(new MonHocSeed());
            modelBuilder.ApplyConfiguration(new LopHocPhanSeed());
        }
    }
}
