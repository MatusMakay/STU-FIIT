<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Http\Response;
use Illuminate\Support\Facades\Storage;

class imageController extends Controller
{
    use Response;

    protected function showImage($filename)
{
   //check image exist or not
   $exists = Storage::disk('storage')->exists('images/'.$filename);
   
   if($exists) {
      
      //get content of image
      $content = Storage::get('storage/images/'.$filename);
      
      //get mime type of image
      $mime = Storage::mimeType('storage/images/'.$filename);
      //prepare response with image content and response code
      $response = Response::make($content, 200);
      //set header 
      $response->header("Content-Type", $mime);
      // return response
      return $response;
   } else {
      abort(404);
   }
}
}
