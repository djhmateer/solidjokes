using System.Threading;
using System.Threading.Tasks;
using Core.DB;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace SolidJokes.Tests {
    public class JokeViewerTests {
        [Fact]
        public void ShowAllJokesHighestRatingFirst_AFakeSessionWithListOfJokes_ShouldReturnListInRatingOrder() {
            var session = new FakeSession();
            session.Jokes.Add(new Joke() { ID = 1, Rating = 2, Title = "Banana"});
            session.Jokes.Add(new Joke() { ID = 2, Rating = 9, Title = "Stick"});
            session.Jokes.Add(new Joke() { ID = 3, Rating = 6, Title = "Airport"});

            var viewer = new JokeViewer(session);
            var result = viewer.ShowAllJokesHighestRatingFirst();
            Assert.Equal("Stick", result[0].Title);
            Assert.Equal("Airport", result[1].Title);
            Assert.Equal("Banana", result[2].Title);
        }

        [Fact]
        public void AddJoke_GivenTitleAndRating_ShouldSaveToDbAndBeAvailableInTheSession()
        {
            var session = new FakeSession();
            var viewer = new JokeViewer(session);

            var result = viewer.AddJoke("sausage", 2);

            Assert.Equal(1, session.Jokes.Count());
            Assert.Equal("sausage", session.Jokes.Single().Title);
            Assert.Equal(2, session.Jokes.Single().Rating);
            Assert.Equal(1, session.SaveChangesCount);
        }
    }

    public class FakeSession : ISession {
        public DbSet<Joke> Jokes { get; set; }
        public int SaveChangesCount { get; private set; }

        public FakeSession() {
            this.Jokes = new TestDbSet<Joke>();
            //this.Jokes = new TestJokeDbSet();
        }

        public int SaveChanges() {
            this.SaveChangesCount++;
            return 1;
        }
    }

    //The Find method is difficult to implement in a generic fashion. 
    //If you need to test code that makes use of the Find method it is easiest to create a test DbSet for each of the entity types that need to support find
    //public class TestJokeDbSet : TestDbSet<Joke> {
    //    public override Joke Find(params object[] keyValues) {
    //        var id = (int)keyValues.Single();
    //        return this.SingleOrDefault(b => b.ID == id);
    //    }
    //} 

    public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity> where TEntity : class {
        ObservableCollection<TEntity> _data;
        IQueryable _query;

        public TestDbSet() {
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public override TEntity Add(TEntity item) {
            _data.Add(item);
            return item;
        }

        public override TEntity Remove(TEntity item) {
            _data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item) {
            _data.Add(item);
            return item;
        }

        public override TEntity Create() {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>() {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local {
            get { return _data; }
        }

        Type IQueryable.ElementType {
            get { return _query.ElementType; }
        }

        Expression IQueryable.Expression {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider {
            get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return _data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() {
            return _data.GetEnumerator();
        }

        IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator() {
            return null;
            //return new TestDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
        }
    }

    internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner) {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression) {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression) {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression) {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken) {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T> {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable) { }

        public TestDbAsyncEnumerable(Expression expression)
            : base(expression) { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator() {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider {
            get { return new TestDbAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T> {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner) {
            _inner = inner;
        }

        public void Dispose() {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken) {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current {
            get { return Current; }
        }
    }
}