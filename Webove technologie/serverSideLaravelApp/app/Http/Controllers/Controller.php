<?php

namespace App\Http\Controllers;

use Illuminate\Foundation\Auth\Access\AuthorizesRequests;
use Illuminate\Foundation\Validation\ValidatesRequests;
use Illuminate\Routing\Controller as BaseController;
use App\Models\product;

class Controller extends BaseController
{
    use AuthorizesRequests, ValidatesRequests;

    public function GEThome(){
        $firstThreeProducts = product::take(3)->get();
        return view('layouts.home.home', ['actionProducts' => $firstThreeProducts]);
    }
}
