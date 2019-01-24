using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOT_PTTK.DAO
{
    public interface IDAO<T>
    {

        /// <summary>
        /// Wstawianie obiektu do bazy danych. Nie można wstawić elementu, który już istnieje
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true- w przypadku gdy operacja się powiodła</returns>
        bool Insert(T model);

        /// <summary>
        /// Modyfikowanie obiektu w bazie danych. Nie można zmodyfikować elementu, który nie istnieje
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true- operacja się powiodła</returns>
        bool Update(T model);

        /// <summary>
        /// Usuwanie elementu z bazy danych. Nie można usunąć elementu, który ne istnieje
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true- operacja się powiodła</returns>
        bool Delete(T model);

        /// <summary>
        /// Odczyt elementów z bazy danych
        /// </summary>
        /// <returns>Zwraca listę obiektów pobraną z bazy danych</returns>
        List<T> GetAll();

        /// <summary>
        /// Sprawdza czy element o podanym identyfikatorze istnieje w bazie danych
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true - jeśli istnieje</returns>
        bool Exists(long id);

        /// <summary>
        /// Zwraca obiekt o podanym indeksie z bazy danych
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Obiekt o podanym indeksie</returns>
        T Find(long id);
    }
}

