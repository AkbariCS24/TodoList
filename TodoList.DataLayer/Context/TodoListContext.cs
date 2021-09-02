using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoList.DataLayer.Models;

namespace TodoList.DataLayer.Context
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options)
            : base(options)
        { }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoItemList> TodoItemLists { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.Property(e => e.TodoItemListID).IsRequired();

                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(100);
            });
            modelBuilder.Entity<TodoItemList>(entity =>
            {
                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }
    }
}
