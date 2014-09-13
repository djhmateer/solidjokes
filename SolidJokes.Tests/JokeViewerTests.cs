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
            session.Jokes.Add(new Joke() {ID=1, Rating = 2});
            session.Jokes.Add(new Joke() {ID=2, Rating = 3});

            var viewer = new JokeViewer(session);

            var result = viewer.ShowAllJokesHighestRatingFirst();

            Assert.NotEmpty(result);
        }
    }

    public class FakeSession : ISession {
        public DbSet<Joke> Jokes { get; set; }

        public FakeSession() {
            this.Jokes = new TestDbSet<Joke>();
        }
    }

    public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
        where TEntity : class {
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
            get {
                return null; //return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
            }
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
}