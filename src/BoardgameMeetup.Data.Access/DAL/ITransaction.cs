using System;

namespace BoardgameMeetup.Data.Access.DAL
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();    
    }
}
