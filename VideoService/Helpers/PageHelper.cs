namespace VideoService.Helpers
{
    public static class PageHelper
    {
        
        public static IEnumerable<int> PageNumbers(int pageNumber, int pageCount)
        {
            if (pageCount <= 5)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    yield return i;
                }
            }
            else
            {
                int midPoint = pageNumber < 3 ? 3
                    : pageNumber > (pageCount - 2) ? pageCount - 2
                    : pageNumber;

                // Нижняя граница (Какая наименьшая страница (кроме первой) будет отображаться снизу в кнопках)
                int lowerBound = midPoint - 2;
                int upperBound = midPoint + 2;

                // Теперь всегда будет отображаться первая страница
                if (lowerBound != 1)
                {
                    // Создаем копию списка, добавляем 1 к началу копии списка
                    // Присваиваем изначальному списку измененную копию

                    yield return 1;
                    //Вставляем ... , если отображаются номера страниц 1 и 3, 1 и 4 и тд
                    if (lowerBound - 1 > 1)
                    {
                        // Добавляем на 1 индекс (после нулевого) число -1
                        // Далее мы отметим, что -1 означает троеточие (...)
                        yield return -1;
                    }
                }

                // от текущей страницы будет показываться 2 предыдущих страницы (кнопки для них)
                // + 2 следующих страницы (по 2 вправо и влево)
                for (int i = midPoint - 2; i <= upperBound; i++)
                {
                    yield return i;
                }

                if (upperBound != pageCount)
                {

                    if (pageCount - upperBound > 1)
                    {
                        yield return -1;
                    }
                    yield return pageCount;
                }

            }
        }
    }
    
}

