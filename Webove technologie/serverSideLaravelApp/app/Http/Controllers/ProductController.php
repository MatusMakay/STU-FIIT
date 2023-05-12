<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreproductRequest;
use App\Http\Requests\UpdateproductRequest;
use App\Models\product;
use Illuminate\Http\Request;
use Illuminate\Support\Collection;
use Illuminate\Validation\ValidationException;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Session;


class ProductController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index(Request $request)
    {
        //Todo verifikacia ? NACO

        $out = new \Symfony\Component\Console\Output\ConsoleOutput();

        $category = $request->query('category');
        $type = $request->query('type');

        //Get products
        if(!$type && !$category){
            $out->writeln("hello");
            $products = product::all();
            $type = "Všetky";
        }
        elseif($type == "All"){
            $products = product::where('category', $category)->get();
            $type = "Všetky";
        }
        else{
            $out->writeln("url params are defined");
            Session::forget('oldParams');
            $params = [
                'category' => $category,
                'type' => $type
            ];
            
            Session::put('oldParams', $params);
            $products = product::where('category', $category)->where('type', $type)->get();

        }

        //When there are no products
        if(!$products){
            return view('layouts.products.products', ['type' => $type, 'productName' => $type, 'products' =>  "Práve nemáme čo hľadáte"]);
        }
        return view('layouts.products.products', ['type' => $type, 'productName' => $type, 'category' =>$category, 'products' =>  $products]);

    }


    public function applyFilters($category, $type, $color, $size, $price) :Collection{
        $query = DB::table('products');
        if($color && !empty($color)){
            $query->whereIn('color', $color);;
        }

        if($size && !empty($size)){
            $query->whereIn('color', $color);
        }

        if($type && $type != "search"){
            $query->where('type', $type);
            $query->where('category', $category);
        }

        if($price){
            if($price == "lowest"){
                $query->orderBy('product_price', 'asc');
            }
            else{
                $query->orderBy('product_price', 'desc');             
            }
            //dd($query);
        }

        $products = $query->get();

        
        
        return $products;
    }
    
    public function sort(Request $request){
        $oldParams = Session::get('oldParams');
        $price = $request->query('price');
        // :(
        try{
            $category = $oldParams['category'];
        }
        catch(\ErrorException $e){
            $category = null;
        }
        try{
            $type = $oldParams['type'];
        }
        catch(\ErrorException $e){
            $type = null;
        }
        try{
            $color = $oldParams['color'];
        }
        catch(\ErrorException $e){
            $color = null;
        }
        try{
            $size =  $oldParams['size'];
        }
        catch(\ErrorException $e){
            $size = null;
        }

       // dd($color, $category, $type, $size);
        $products=$this->applyFilters($category, $type, $color, $size, $price);

        return view('layouts.products.products', ['type' => $type, 'productName' => $type, 'category' => $category, 'products' =>  $products]);

    }

    public function filter(Request $request){
        $params =  $request->query();
        $category = $request->query('category');
        $type = $request->query('type');
        $color = $request->query('color');
        $size =  $request->query('size');

        Session::forget('oldParams');
        Session::put('oldParams', $params);

        $products=$this->applyFilters($category, $type, $color, $size, null);

        return view('layouts.products.products', ['type' => $type, 'productName' => $type, 'category' => $category, 'products' =>  $products]);
    }
       

    public function search(Request $request){
        // Get the search value from the request
        $search = $request->input('search');

        // Search in the title and body columns from the posts table
        $products = product::query()
            ->where('item_name', 'iLIKE', "%{$search}%")
            ->orWhere('category', 'iLIKE', "%{$search}%")
            ->orWhere('brand', 'iLIKE', "%{$search}%")
            ->orWhere('type', 'iLIKE', "%{$search}%")
            ->orWhere('brand', 'iLIKE', "%{$search}%")
            ->get();

        // Return the search view with the resluts compacted
        return view('layouts.products.products', ['productName' => 'Vyhľadaj', 'category' => 'Nájdené', 'type' => 'search' ,'products' =>  $products]);
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {

    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreproductRequest $request)
    {
        dd($request);
    }

    /**
     * Display the specified resource.
     */
    public function show(product $product)
    {
        return view('layouts.products.product', ['productType' => $product->type, 'productImg' => $product->product_img ,'productName' => $product->item_name, 'productDescription'=>$product->description,  'productPrice' => $product->product_price, 'productId' => $product->id, 'count' => 5]);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(product $product)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateproductRequest $request, product $product)
    {
        dd($request, $product);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(product $product)
    {
        dd($product);
    }

    public function addItemToCart(Request $request)
    {
        if(!($request->has('color-choice') ||  $request->has('size-choice'))){
            throw  ValidationException::withMessages([
                'you didnt choose a color or size'
            ]);
        }
        $product = product::where('id', $request->productId)->where('number_products', '>=', 0 )->first();
        if($product->number_products == 0){
            return view('layouts.products.product', ['productType' => $product->type, 'productImg' => $product->product_img ,'productName' => $product->item_name, 'count' => $product->number_products ,'productDescription'=>$product->description,  'productPrice' => $product->product_price, 'productId' => $product->id]);
        }
        // just for a test
        else{
            if(!session()->has('cart')){
                session()->put('cart', []);
            }
            if(!session()->has('count')){
                session()->put('count', []);
            }


            session()->push('cart', $request->productId);
            $product->number_products = $product->number_products-1;
            $product->save();
            return view('layouts.products.product', ['productType' => $product->type, 'productImg' => $product->product_img ,'productName' => $product->item_name, 'count' => $product->number_products ,'productDescription'=>$product->description,  'productPrice' => $product->product_price, 'productId' => $product->id]);
        }



    }
}
