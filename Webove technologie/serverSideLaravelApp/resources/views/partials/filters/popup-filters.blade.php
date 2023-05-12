


    <div class="flex items-center">
        <div class="inline-block text-left">
            <div>
                <button onclick="menuBtnClick()" id="sort-btn" type="button"
                    class="inline-flex justify-center text-m font-medium text-gray-900 " id="menu-button">
                    <span class="sm:hidden md:block ">
                        Usporiadaj
                    </span>
                    <img src="img/icons/chevron-down-outline.svg" alt="" class="w-6 h-6 sort-icon">
                </button>
            </div>

            <div id="sort-menu"
                class="hidden absolute right-0 z-10 mt-2 w-60 origin-top-right rounded-md bg-white shadow-2xl ring-1 ring-black ring-opacity-5 focus:outline-none sm:mr-2 md:mr-6 xl:mr-[6vw] "
                role="menu" aria-orientation="vertical" aria-labelledby="menu-button" tabindex="-1">
                <div class="py-1" role="none">
                    <!-- <a href="#"
                        class="text-gray-500 block px-4 py-2 text-sm hover:bg-orange-100 dark:hover:bg-gray-600 dark:hover:text-white"
                        role="menuitem" tabindex="-1" id="menu-item-0">Najviac obľúbené</a>

                    <a href="#"
                        class="text-gray-500 block px-4 py-2 text-sm hover:bg-orange-100 dark:hover:bg-gray-600 dark:hover:text-white"
                        role="menuitem" tabindex="-1" id="menu-item-1">Najlepšie hodnotené</a>

                    <a href="#"
                        class="text-gray-500 block px-4 py-2 text-sm hover:bg-orange-100 dark:hover:bg-gray-600 dark:hover:text-white"
                        role="menuitem" tabindex="-1" id="menu-item-2">Najnovšie</a> -->
                    
                    <a href="/sort?price=lowest"
                        class="text-gray-500 block px-4 py-2 text-sm hover:bg-orange-100 dark:hover:bg-gray-600 dark:hover:text-white"
                        role="menuitem" tabindex="-1" id="menu-item-3">Cena: Od najnižšej</a>

                    <a href="/sort?price=highest"
                        class="text-gray-500 block px-4 py-2 text-sm hover:bg-orange-100 dark:hover:bg-gray-600 dark:hover:text-white"
                        role="menuitem" tabindex="-1" id="menu-item-4">Cena: Od najvyššej</a>
                </div>
            </div>
        </div>
    </div>