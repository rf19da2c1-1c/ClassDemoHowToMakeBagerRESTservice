using System;
using System.Collections.Generic;
using System.Text;

namespace BagerLib.DBUitl
{
    public interface IManageDB<T> where T : IDBElement
    {
        IList<T> HentAlle();
        T HentEn(int key);
        bool Opret(T item);
        bool Update(int key, T item);
        T Delete(int key);

    }
}
