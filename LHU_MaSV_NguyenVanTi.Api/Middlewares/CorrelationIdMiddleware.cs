using Serilog.Context;

namespace LHU_MaSV_NguyenVanTi.Api.Middlewares
{
    /// <summary>
    /// Middleware quản lý mã định danh truy vết (Correlation ID) cho từng yêu cầu HTTP.
    /// Giúp theo vết và liên kết chuỗi sự kiện, bản ghi nhật ký của cùng một phiên giao dịch.
    /// </summary>
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        public const string CorrelationIdHeaderName = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Kiểm tra xem Client có gửi sẵn mã X-Correlation-ID không. Nếu không, tự sinh mã mới (GUID).
            var correlationId = context.Request.Headers[CorrelationIdHeaderName].FirstOrDefault();
            if (string.IsNullOrWhiteSpace(correlationId))
            {
                correlationId = Guid.NewGuid().ToString("N");
            }

            // 2. Gắn Correlation ID vào Header phản hồi gửi về cho Client (để Client tra cứu khi báo lỗi)
            context.Response.Headers[CorrelationIdHeaderName] = correlationId;

            // 3. Đẩy thuộc tính "CorrelationId" vào Serilog LogContext cho toàn bộ các lớp Controller/Service bên dưới
            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(context);
            }
        }
    }
}
