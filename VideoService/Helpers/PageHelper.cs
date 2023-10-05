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
        
        // БЫЛО БЕЗ yield и переменных-границ
        /*
        public static IEnumerable<int> PageNumbers(int pageNumber, int pageCount) {


            // pageNumber - номер текущей отображаемой страницы
            // pageCount - число отображаемых кнопок для страниц

            // Список отображаемых номеров страниц
            List<int> pages = new List<int>();

            int midPoint = pageNumber < 3 ? 3
                : pageNumber > (pageCount - 2) ? pageCount - 2
                : pageNumber;

            // ТО ЖЕ САМОЕ ЧТО И

            //int midPount;

            //if (pageNumber < 3) // текущая страница - это середина 
            //{
            //    midPoint = 3;
            //}

            //else if (pageNumber > pageCount - 2) // Чтобы пользователь бежал только по непустым страницам
            //{
            //    midPoint = pageCount - 2;
            //}

            //else
            //{
            //    midPoint = pageNumber;
            //}


            for (int i = midPoint - 2; i <= midPoint + 2; i++)
            {
                pages.Add(i);
            }

            // Теперь всегда будет отображаться первая страница
            if (pages[0] != 1)
            {
                // Создаем копию списка, добавляем 1 к началу копии списка
                // Присваиваем начальному списку измененную копию
                pages = pages.Prepend(1).ToList();

                //Вставляем ... , если отображаются номера страниц 1 и 3, 1 и 4 и тд
                if ((pages[1] - pages[0]) > 1)
                {
                    // Добавляем на 1 индекс (после нулевого) число -1
                    // Далее мы отметим, что -1 означает троеточие (...)
                    pages.Insert(1, -1);
                }
            }

            if (pages[pages.Count - 1] != pageCount)
            {
                //pages.Add(pageCount);
                if ((pages[pages.Count - 1] - pages[pages.Count - 2]) > 1)
                {
                    // Добавляем на 1 индекс (после нулевого) число -1
                    // Далее мы отметим, что -1 означает троеточие (...)
                    pages.Insert(pages.Count -1, -1);
                }
            }

            return pages.AsEnumerable();

        }*/
    }
    
}

