# GeometryDrawer
This draws an arbitrary geometry and repeats it.

Intention is to build an arbitrary image with right triangles such that for a given row (A-F) and column (1-12), you can produce a layout of right triangles taking up a square.

When running the web application, it can be called from http://<your server:port>/api/Image?row=<desired row>&column=<desired column>. To get the row and column of any one triangle, a GetRowAndColumn method can be called on the repeater object to get a tuple of the <string, int>.

Approximate coding time: 8.5 hrs
Language: C#
Date: 8/21
