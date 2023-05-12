@extends('app')

@section('title')
    Sumár
@endsection
@section('content')

<div class="flex justify-center my-6">
    <div class="flex flex-col w-full p-8 text-gray-800 bg-white shadow-lg pin-r pin-y md:w-4/5 lg:w-4/5">
      <div class="flex-1">
        <table class="w-full mt-6 text-sm lg:text-base" cellspacing="0">
          <thead>
            <tr class="h-12 uppercase">
              <th class="hidden md:table-cell"></th>
              <th class="text-left">Produkt</th>
              <th class="hidden text-right md:table-cell">Cena za kus</th>
            </tr>
          </thead>
          <tbody>
            @foreach($products as $product)
                <tr>
                    <td class="hidden pb-4 md:table-cell">
                        <img src="img/products/{{$product->id}}" class="w-20 rounded" alt="Thumbnail">
                    </td>
                    <td>
                        <p class="mb-2 md:ml-4">{{$product->name}}</p>

                    </td>
                    <td class="hidden text-right md:table-cell">
                        <span class="text-sm lg:text-base font-medium">
                        {{$product->product_price}}$
                        </span>
                    </td>
                </tr>
            @endforeach
          </tbody>
        </table>

        <hr class="pb-6 mt-6">

        <div class="my-4 mt-6 -mx-2 lg:flex">
          <div class="lg:px-2 lg:w-1/2">
          <div class="lg:px-2 lg:w-1/2">
            <div class="p-4">

              <div class="text-gray-700">
                <div class="grid md:grid-cols-1 text-sm">
                    <div class="grid grid-cols-2">
                        <div class="px-4 py-2 font-semibold">Addresa</div>
                        <div class="px-4 py-2">{{$userAdress}}</div>
                    </div>
                    <div class="grid grid-cols-2">
                        <div class="px-4 py-2 font-semibold">Email</div>
                        <div class="px-4 py-2">{{$userEmail}}</div>
                    </div>
                    <div class="grid grid-cols-2">
                      <div class="px-4 py-2 font-semibold">Doručenie</div>
                      <div class="px-4 py-2"> {{$deliveryMethod}}</div>
                  </div>

                </div>
            </div>

                <div class="flex justify-between border-b">
                  <div class="lg:px-4 lg:py-2 m-2 text-lg lg:text-xl font-bold text-center text-gray-800">
                    Spolu
                  </div>
                  <div class="lg:px-4 lg:py-2 m-2 lg:text-lg font-bold text-center text-gray-900">
                    {{$totalPrice}}
                  </div>

                  <div class="flex flex-row p-2  mt-3 pb-5 justify-center gap-3 lg:font-medium">

                    <a href="/"
                            class=" flex items-center text-white bg-gray-700 focus:ring-4 grid-cols-1 focus:outline-none focus:ring-gray-300  rounded-lg lg:px-5 sm:px-2   sm:py-2.5 lg:py-2.5 md:py-4 bg-gray-600 hover:bg-orange-400 dark:focus:ring-gray-800 ">Domov
                    </a>
                </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
@endsection
