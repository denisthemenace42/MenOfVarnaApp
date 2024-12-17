using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

public static class MockDbSetHelper
{
    public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => data.ToList().Add(s));

        return mockSet;
    }

    public class MockHttpSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionStorage = new Dictionary<string, byte[]>();

        public IEnumerable<string> Keys => _sessionStorage.Keys;

        public bool IsAvailable => true;

        public string Id => Guid.NewGuid().ToString();

        public void Clear() => _sessionStorage.Clear();

        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public void Remove(string key) => _sessionStorage.Remove(key);

        public void Set(string key, byte[] value) => _sessionStorage[key] = value;

        public bool TryGetValue(string key, out byte[] value) => _sessionStorage.TryGetValue(key, out value);

        public void SetString(string key, string value) => Set(key, System.Text.Encoding.UTF8.GetBytes(value));

        public string GetString(string key) =>
            _sessionStorage.TryGetValue(key, out var value) ? System.Text.Encoding.UTF8.GetString(value) : null;
    }

}
