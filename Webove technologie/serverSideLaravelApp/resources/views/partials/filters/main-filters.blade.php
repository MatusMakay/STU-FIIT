    <h3 class=" text-3xl text-center">
        Filtre
    </h3>
    <div class="border-b border-gray-200 py-6">
        <h3 class="-my-3 flow-root flex w-full items-center justify-between bg-white py-3 text-sm text-gray-400 hover:text-gray-500">
            <span class="text-xl text-gray-900">Color</span>
        </h3>
        <div class="pt-6" id="filter-section-0">
            <div class="space-y-4">
                <div class="flex items-center">
                    <input id="filter-color-0" name="color[]" value="White" type="checkbox"
                        class="h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-color-0" class="ml-3 text-sm text-gray-600">Biela</label>
                </div>

                <div class="flex items-center">
                    <input id="filter-color-2" name="color[]" value="Black" type="checkbox" 
                        class=" accent-orange h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-color-2" class="ml-3 text-sm text-gray-600">Čierna</label>
                </div>

                <div class="flex items-center">
                    <input id="filter-color-3" name="color[]" value="Pink" type="checkbox"
                        class="accent-orange h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-color-3" class="ml-3 text-sm text-gray-600">Ružová</label>
                </div>
            </div>
        </div>
    </div>

    <!-- <div class="border-b border-gray-200 py-6">
        <h3 class="-my-3 flow-root flex w-full items-center justify-between bg-white py-3 text-sm text-gray-400 hover:text-gray-500">
            <span class="text-xl text-gray-900">Typ trička</span>
        </h3>
        <div class="pt-6" id="filter-section-1">
            <div class="space-y-4">
                <div class="flex items-center">
                    <input id="filter-category-1" name="type[]" value="LongSleeve" type="checkbox"
                        class="h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500 accent-orange">
                    <label for="filter-category-1" class="ml-3 text-sm text-gray-600">S dlhým
                        rukávom</label>
                </div>

                <div class="flex items-center">
                    <input id="filter-category-2" name="type[]" value="ShortSleeve" type="checkbox"
                        class="accent-orange h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-category-2" class="ml-3 text-sm text-gray-600">S krátkym
                        rukávom</label>
                </div>

                <div class="flex items-center">
                    <input id="filter-category-3" name="type[]" value="TooBig"
                        type="checkbox"
                        class="accent-orange h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-category-3" class="ml-3 text-sm text-gray-600">Príliš
                        veľké</label>
                </div>
            </div>
        </div>
    </div> -->

    <div class="border-b border-gray-200 py-6">
        <h3 class="-my-3 flow-root flex w-full items-center justify-between bg-white py-3 text-sm text-gray-400 hover:text-gray-500">
                <span class="text-xl text text-gray-900">Veľkosť</span>
        </h3>
        <div class="pt-6" id="filter-section-2">
            <div class="space-y-4">
                <div class="flex items-center">
                    <input id="filter-size-0" name="size[]" value="S" type="checkbox"
                        class="accent-orange h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-size-0" class="ml-3 text-sm text-gray-600">S</label>
                </div>

                <div class="flex items-center">
                    <input id=" filter-size-1" name="size[]" value="M" type="checkbox"
                        class="accent-orange h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-size-1" class="ml-3 text-sm text-gray-600">M</label>
                </div>

                <div class="flex items-center">
                    <input id="filter-size-2" name="size[]" value="L" type="checkbox"
                        class="accent-orange h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-size-2" class="ml-3 text-sm text-gray-600">L</label>
                </div>

                <div class="flex items-center">
                    <input id="filter-size-3" name="size[]" value="XL" type="checkbox"
                        class="accent-orange h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-size-3" class="ml-3 text-sm text-gray-600">XL</label>
                </div>

                <div class="flex items-center">
                    <input id="filter-size-4" name="size[]" value="XXL" type="checkbox"
                        class="accent-orange h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500">
                    <label for="filter-size-4" class="ml-3 text-sm text-gray-600">XXL</label>
                </div>
            </div>
        </div>
    </div>
    <div class="w-full text-center">
        <button
            class="w-10 h-10 bg-gray-700 px-2 py-2 mt-4 text-white rounded-full  hover:bg-orange-600 bg-[url('img/icons/checkmark-outline.svg')]">
        </button>
    </div>
</form>