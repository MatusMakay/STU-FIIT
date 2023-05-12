@extends('app')

@section('title')
    Doručovacia Adresa
@endsection

@section('content')
    <main class=" w-[90%] mt-16 mx-auto">
        <div class="flex">
            <div class="m-auto md:w-full lg:w-[35%]">
                <div>
                    <div id="shipping-box" class="mt-5 bg-white rounded-lg pt-3">
                        <div class="flex flex-row w-full py-5 pl-5 sm:text-2xl  lg:text-3xl  md:text-5xl">
                            <div class="sm:w-[79%] md:w-[70%] lg:w-[70%] mx-auto flex sm:items-center ">
                                <img src="../img/payment/location.svg" alt=""
                                    class=" lg:w-8  lg:h-8 sm:w-8 sm:h-8 md:w-12 md:h-12  sort-icon mr-1">
                                <h1 class="inline  font-semibold leading-none ">
                                    Dodacia Adresa
                                </h1>
                            </div>
                        </div>
                        <form Method="POST" action="{{route('checkInfo')}}">
                            @csrf
                            <div class="px-5 pb-3 md:text-2xl lg:text-base">
                                <input placeholder="Email" name="email"
                                    class=" text-black placeholder-gray-600 w-full px-4 py-2.5 mt-2  bg-gray-200 rounded-lg ">
                                <input placeholder="Adresa" name="address"
                                    class=" text-black placeholder-gray-600 w-full px-4 py-2.5 mt-2 rounded-lg bg-gray-200 ">
                                <div class="lg:max-2xl:flex">
                                    <div class="lg:max-2xl:flex-grow lg:max-2xl:w-1/4 lg:max-2xl:pr-2 ">
                                        <input placeholder="PSČ" name="psc"
                                            class=" text-black placeholder-gray-600 w-full px-4 py-2.5 mt-2 rounded-lg bg-gray-200 ">
                                    </div>
                                    <div class="lg:max-2xl:flex-grow"><input placeholder="Mesto" name="city"
                                            class=" text-black placeholder-gray-600 w-full px-4 py-2.5 mt-2 rounded-lg bg-gray-200 ">
                                    </div>
                                </div>
                                <!-- <div class=" pt-3 mt-2">
                                    <label for="safeAdress"
                                        class=" flex items-center block ml-2 lg:text-sm sm:text-l text-gray-900">
                                        <input type="checkbox"
                                            class="accent-orange  sm:w-4 sm:h-4 lg:w-4 lg:h-4 md:w-6 md:h-6 text-black bg-gray-300 border-none rounded-md">
                                        <span class="pl-2 pt-1">
                                            Predvolená adresa pre tvoj profil
                                        </span>
                                    </label>
                                </div> -->
                            </div>

                            <hr class="mt-4">

                            <div class="flex flex-row p-2  mt-3 pb-5 justify-center gap-3 lg:font-medium md:text-2xl lg:text-[17px]
                           ">
                                <button type="button"
                                    class="w-full flex items-center text-white bg-gray-700 focus:ring-4 focus:outline-none focus:ring-gray-300  rounded-lg lg:px-5 sm:px-2   sm:py-2.5 lg:py-2.5 md:py-4 bg-gray-600 hover:bg-orange-400 dark:focus:ring-gray-800  ">
                                    <img src="../img/payment/cart-outline.svg" alt=""
                                        class="lg:w-6 lg:h-6 sm:w-8 sm:h-8 md:block sm:hidden sort-icon md:w-10 md:h-10 invert">
                                    <span class="lg:max-2xl:pl-2 mx-1"><a href="/payment">Výber platby</a></span>
                                </button>

                                    <button type="submit"
                                    class="w-full flex items-center text-white bg-gray-700 focus:ring-4 focus:outline-none focus:ring-gray-300 rounded-lg  lg:px-5 sm:px-2 sm:py-2.5 lg:py-2.5 md:py-4  bg-gray-600 hover:bg-orange-400 dark:focus:ring-gray-800 ">
                                    <img src="../img/payment/card-outline.svg"
                                        class="sm:w-8 sm:h-8 md:block sm:hidden sort-icon md:w-12 md:h-12 lg:w-8 lg:h-8  md:pr-2 invert">
                                        Kúpiť
                                    </button>

                            </div>
                        </form>

                        @if(isset($error))
                        <div class="alert alert-danger">
                            {{ $error }}
                        </div>
                        @endif
                        @if($errors->all())
                        <div class="alert alert-danger">
                            <ul>
                                @foreach($errors->all() as $error)
                                <li>{{$error}}</li>
                                @endforeach
                            </ul>
                        </div>
                        @endif
            </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
@endsection
