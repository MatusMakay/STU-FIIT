@extends('app')

@section('title')
    Platobne Detaily
@endsection

@section('content')
    <main class="lg:w-[60%] sm:w-[100%] mx-auto mt-14 sm-max:h-[800px] ">
        <div class="flex w-full">
            <div id="shipping-box" class=" bg-white rounded-lg pt-3 w-full">
                <form method="POST" action="{{route('check')}}">
                @csrf
                <div class="flex flex-row">
                    <div class="w-full py-5 pl-5">
                        <div class="mx-auto flex text-center items-center">
                            <img src="../img/payment/secure-delivery-icon.svg" alt=""
                                class="lg:w-9 lg:h-9 sm:w-12 sm:h-12 sm:mt-1  md:w-16 md:h-16 md:pt-1 lg:pt-0  sort-icon mr-1">
                            <h1 class="inline lg:text-2xl sm:text-4xl md:text-5xl font-semibold leading-none pl-1 ">
                                Doprava
                            </h1>
                        </div>
                    </div>
                </div>
                <div class="px-5 pb-3">
                    <div
                        class="w-full flex flex-row  items-center sm:gap-5 text-black bg-gray-200 rounded-md px-2 py-2 ">
                        <div class="w-[4%]">
                            <input type="checkbox" name="out" value="pick up"
                                class=" accent-orange sm:h-5 sm:w-5 lg:h-5 lg:w-5  md:h-7 md:w-7 border border-gray-400 rounded-md mt-1">
                        </div>
                        <div class="w-[8%]">
                            <img src="../img/payment/person.svg" alt=""
                                class="sm:w-8 sm:h-8 lg:w-8 lg:h-8 md:w-10 md:h-10 sort-icon mr-1  pl-1">
                        </div>
                        <div class="w-[60%]">
                            <span class="sm:text-[17px] lg:text-[18px] md:text-2xl">
                                Vyzdvihnutie na prevádzke
                            </span>
                        </div>
                    </div>

                    <div
                        class="w-full flex flex-row sm:gap-5  mt-1 items-center text-black bg-gray-200 rounded-md px-2 py-2 ">
                        <div class="w-[4%]">
                            <input type="checkbox" name="home" value="home delivery"
                                class="accent-orange sm:h-5 sm:w-5 lg:h-5 lg:w-5  md:h-7 md:w-7 border border-gray-400 rounded-md mt-2">
                        </div>
                        <div class="w-[8%]">
                            <img src="../img/payment/home.svg" alt=""
                                class="sm:w-8 sm:h-8 lg:w-8 lg:h-8 md:w-10 md:h-10 sort-icon mr-1 pt-1 pl-1 ">
                        </div>
                        <div class="w-[60%]">
                            <span class="sm:text-[17px] lg:text-[18px] md:text-2xl">
                                Doprava domov
                            </span>
                        </div>
                    </div>
                </div>

                <div class="flex flex-row">
                    <div class="w-full py-5 pl-5">
                        <div class="mx-auto flex text-center items-center">
                            <img src="../img/payment/secure-delivery-icon.svg" alt=""
                                class="lg:w-9 lg:h-9 sm:w-12 sm:h-12  md:w-16 md:h-16 md:pt-1 sm:mt-1 lg:pt-0 sort-icon mr-1 ">
                            <h1 class="inline lg:text-2xl sm:text-4xl md:text-5xl font-semibold leading-none pl-1 ">
                                Spôsob platby
                            </h1>
                        </div>
                    </div>
                </div>
                <div class="px-5 pb-3">
                    <div class="w-full flex flex-row items-center gap-5 text-black bg-gray-200 rounded-md px-2 py-2 ">
                        <div class="w-[4%]">
                            <input type="checkbox" name="card" value="card"
                                class="accent-orange sm:h-5 sm:w-5 lg:h-5 lg:w-5  md:h-7 md:w-7 border border-gray-400 rounded-md mt-1">
                        </div>
                        <div class="w-[8%]">
                            <img src="../img/payment/person.svg" alt=""
                                class="sm:w-8 sm:h-8 lg:w-8 lg:h-8 md:w-10 md:h-10  sort-icon mr-1  pl-1">
                        </div>
                        <div class="w-[60%]">
                            <span class="sm:text-[17px] lg:text-[18px] md:text-2xl">
                                Kreditna karta
                            </span>
                        </div>
                    </div>
                    <div
                        class="w-full flex flex-row items-center gap-5 text-black bg-gray-200 rounded-md px-2 py-2 mt-1">
                        <div class="w-[4%]">
                            <input type="checkbox" name="money" value="cash"
                                class="accent-orange sm:h-5 sm:w-5 lg:h-5 lg:w-5  md:h-7 md:w-7 border border-gray-400 rounded-md mt-2">
                        </div>
                        <div class="w-[8%]">
                            <img src="../img/payment/home.svg" alt=""
                                class="sm:w-8 sm:h-8 lg:w-8 lg:h-8 md:w-10 md:h-10  sort-icon mr-1 pt-1 pl-1 ">
                        </div>
                        <div class="w-[60%]">
                            <span class="sm:text-[17px] lg:text-[18px] md:text-2xl">
                                Hotovosť
                            </span>
                        </div>
                    </div>
                    <hr class="mt-2">
                    <div
                        class="flex flex-row mt-4 w-full  sm:justify-between md:justify-center lg:text-[16px] md:text-2xl sm:text-sm">
                        <button type="button"
                            class="flex items-center lg:w-[30%] sm:w-[38%]  md:w-[40%] hover:bg-orange-400
                            text-white bg-gray-700 focus:ring-4 focus:outline-none focus:ring-gray-300 font-medium rounded-lg px-5 py-2.5 bg-gray-600 hover:bg-orange dark:focus:ring-gray-800 mr-4">
                            <img src="../img/payment/cart-outline.svg" alt="" class="w-6 h-6 sort-icon invert">
                            <span class="pl-2 mx-1"><a href="/cart">Košík</a></span>
                        </button>

                        <button type="submit" class="flex items-center  lg:w-[30%]  md:w-[40%] hover:bg-orange-400
                            text-white bg-gray-700 focus:ring-4 focus:outline-none focus:ring-gray-300 font-medium rounded-lg px-5 py-2.5 bg-gray-600 hover:bg-orange dark:focus:ring-gray-800
                            ">
                            <img src="../img/payment/card-outline.svg" alt="" class="w-6 h-6 sort-icon invert">
                            <span class="pl-2 mx-1">Doručenie</span>
                        </button>
                    </div>
                </div>
                </form>
                @if($errors->all())
                        <div class="alert alert-danger center">
                            <ul>
                                @foreach($errors->all() as $error)
                                <li>{{$error}}</li>
                                @endforeach
                            </ul>
                        </div>
                        @endif
        </div>

    </main>
@endsection
