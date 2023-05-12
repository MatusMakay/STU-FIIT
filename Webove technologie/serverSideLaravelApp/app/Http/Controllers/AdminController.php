<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreadminRequest;
use App\Http\Requests\UpdateadminRequest;
use App\Models\admin;
use App\Models\product;
use Illuminate\Http\Request;
use App\Http\Requests\StoreproductRequest;
use App\Http\Requests\UpdateproductRequest;


class AdminController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $all = product::all();
        return view('layouts.admin.admin', ['products' => $all]);
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        dd($request);
        $name = $request->input('name');
        $productDescription = $request->input('productDescrition');
        $productCount = $request->input('productCount');
        $productColor = $request->input('productColor');
        $productPrice = $request->input('productPrice');
        $productSize = $request->input('productSize');
        $protuctType = $request->input('protuctType');
        $productCategory = $request->input('productCategory');

        $product = product::new();
        $product->item_name = $name;
        $product->description = $productDescription;
        $product->product_price = $productPrice;
        $product->number_products = $productCount;
        $product->color = $productColor;
        $product->size = $productSize;
        $product->type = $protuctType;
        $product->category = $productCategory;

        $product->save();

        $all = product::all();
        return view('layouts.admin.admin', ['products' => $all]);
    }

    /**
     * Display the specified resource.
     */
    public function show(Request $request)
    {
        dd($request);
       
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Request $request)
    {
        dd($request);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request)
    {
        $name = $request->input('name');
        $productDescription = $request->input('productDescrition');
        $productCount = $request->input('productCount');
        $productId = $request->input('productId');
        $productColor = $request->input('productColor');
        $productPrice = $request->input('productPrice');


        $productUpdate = Product::find($productId);
        $productUpdate->item_name = $name;
        $productUpdate->description = $productDescription;
        $productUpdate->number_products = $productCount;
        $productUpdate->color = $productColor;
        $productUpdate->product_price = $productPrice;

        //$request->session()->flash('message', 'Úloha bola úspešne zmenená.');

        $productUpdate->save();

        $all = product::all();
        return view('layouts.admin.admin', ['products' => $all]);

    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy($id)
    {
        $product = Product::find($id);

        $product->delete();
        $all = product::all();
        return view('layouts.admin.admin', ['products' => $all]);
    }
}
