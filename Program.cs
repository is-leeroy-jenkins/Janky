// ******************************************************************************************
//     Assembly:                Janky
//     Author:                  Terry D. Eppler
//     Created:                 03-18-2025
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        03-18-2025
// ******************************************************************************************
// <copyright file="Program.cs" company="Terry D. Eppler">
//     Janky is Blazor project template
// 
//     Copyright �  2020 Terry D. Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the �Software�),
//    to deal in the Software without restriction,
//    including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense,
//    and/or sell copies of the Software,
//    and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
// 
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
// 
//    THE SOFTWARE IS PROVIDED �AS IS�, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//    DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//    DEALINGS IN THE SOFTWARE.
// 
//    You can contact me at:  terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   Program.cs
// </summary>
// ******************************************************************************************

using Janky.Components;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddRazorComponents( )
    .AddInteractiveServerComponents( );

var app = builder.Build( );

// Configure the HTTP request pipeline.
if( !app.Environment.IsDevelopment( ) )
{
    app.UseExceptionHandler( "/Error", true );

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts( );
}

app.UseHttpsRedirection( );
app.UseStaticFiles( );
app.UseAntiforgery( );
app.MapRazorComponents<App>( )
    .AddInteractiveServerRenderMode( );

app.Run( );