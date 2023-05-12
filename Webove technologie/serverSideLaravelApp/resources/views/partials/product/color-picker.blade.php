<div>
    <h3 class="text-sm font-medium text-gray-900">Farba</h3>

    <fieldset class="mt-4">
        <legend class="sr-only">Vyber farby</legend>
        <div class="flex items-center space-x-3">
            <!--
                Active and Checked: "ring ring-offset-1"
                Not Active and Checked: "ring-2"
            -->
            <label class=" -m-0.5 flex cursor-pointer items-center justify-center rounded-full p-0.5 focus:outline-none ring-gray-400">
                <input type="radio" name="color-choice" value="White" class="sr-only" aria-labelledby="color-choice-0-label">
                <span id="color-choice-0-label" class="sr-only"> White </span>
                <span aria-hidden="true" class="h-8 w-8 bg-white rounded-full border border-black border-opacity-10"></span>
            </label>

            <!--
                Active and Checked: "ring ring-offset-1"
                Not Active and Checked: "ring-2"
            -->
            <label class=" -m-0.5 flex cursor-pointer items-center justify-center rounded-full p-0.5 focus:outline-none ring-gray-400">
                <input type="radio" name="color-choice" value="Pink" class="sr-only" aria-labelledby="color-choice-1-label">
                <span id="color-choice-1-label" class="sr-only"> Pink </span>
                <span aria-hidden="true" class="h-8 w-8 bg-pink-200 rounded-full border border-black border-opacity-10"></span>
            </label>

            <!--
                Active and Checked: "ring ring-offset-1"
                Not Active and Checked: "ring-2"
            -->
            <label class=" -m-0.5 flex cursor-pointer items-center justify-center rounded-full p-0.5 focus:outline-none ring-gray-900">
                <input type="radio" name="color-choice" value="Black" class="sr-only" aria-labelledby="color-choice-2-label">
                <span id="color-choice-2-label" class="sr-only"> Black </span>
                <span aria-hidden="true" class="h-8 w-8 bg-gray-900 rounded-full border border-black border-opacity-10"></span>
            </label>
        </div>
    </fieldset>
</div>