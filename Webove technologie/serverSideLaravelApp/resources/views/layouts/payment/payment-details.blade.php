@extends('app')

@section('title')
    Košík
@endsection

@section('content')

<main class="w-[80%] mt-24 mx-auto">
        <div class="flex">
            <div class="m-auto">
                <div>
                    <div id="shipping-box" class="mt-5 bg-white rounded-lg pt-3">
                        <div class="flex flex-row">
                            <div class="w-full py-5 pl-5">
                                <div class="w-[60%] sm-max:w-[90%] mx-auto flex text-center">
                                    <img src="../img/payment/location.svg" alt="" class="w-6 h-6 sort-icon mr-1">
                                    <h1 class="inline md:text-2xl font-semibold leading-none sm-max:text-[12px]">
                                        Detaily platby kartou
                                    </h1>
                                </div>
                            </div>
                        </div>
                        <div class="px-5 pb-3">

                            <input placeholder="Čislo karty" id="card-number" name="card-number" required
                                class=" text-black placeholder-gray-600 w-full px-4 py-2.5 mt-2 text-base  bg-gray-200 rounded-lg ">
                            <input placeholder="Meno držiteľa karty" type="text" id="card-holder-name"
                                name="card-holder-name" required
                                class=" text-black placeholder-gray-600 w-full px-4 py-2.5 mt-2 text-base  bg-gray-200 rounded-lg ">
                            <div class="flex">
                                <div class="flex-grow w-1/4 pr-2">
                                    <input placeholder="Expiračný dátum" type="month" id="expiration-date"
                                        name="expiration-date" required
                                        class=" text-black placeholder-gray-600 w-full px-4 py-2.5 mt-2 text-base  rounded-lg bg-gray-200 ">
                                </div>
                                <div class="flex-grow">
                                    <input placeholder="CVV" type="number" id="cvv" name="cvv" required
                                        class=" text-black placeholder-gray-600 w-full px-4 py-2.5 mt-2 text-base rounded-lg bg-gray-200">
                                </div>
                            </div>
                            <!-- <div class=" pt-3 mt-2">
                                <label for="safeAdress" class=" flex items-center block ml-2 text-sm text-gray-900">
                                    <input type="checkbox"
                                        class="w-4 h-4 text-black bg-gray-300 border-none rounded-md">
                                    <span class="pl-2 pt-1">
                                        Ulož ako predvolenú kartu pre tvoj profil
                                    </span>
                                </label>
                            </div> -->
                        </div>
                        </form>
                        <hr class="mt-4">
                        <div class="flex flex-row p-2 pl-5 mt-3 ml-4 pb-5">

                            <button type="button"
                                class="flex items-center text-white bg-gray-700 focus:ring-4 focus:outline-none focus:ring-gray-300 font-medium rounded-lg text-sm px-2 py-2.5  bg-gray-600 hover:bg-orange-400 dark:focus:ring-gray-800 mr-4">
                                <img src="../img/payment/cart-outline.svg" alt="" class="w-6 h-6 sort-icon invert">
                                <span class="pl-2 mx-1"><a href="payment/delivery">Naspať</a></span>
                            </button>

                            <button type="submit "
                                class="flex items-center text-white bg-gray-700 focus:ring-4 focus:outline-none focus:ring-gray-300 font-medium rounded-lg text-sm px-2 py-2.5  bg-gray-600 hover:bg-orange-400 dark:focus:ring-gray-800 mr-4">
                                <img src="../img/payment/card-outline.svg" alt="" class="w-6 h-6 sort-icon invert">
                                    <span class="pl-2 mx-1">Kúpiť</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </main>

@endsection
