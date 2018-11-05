﻿/*
 * Copyright © 2015 - 2018 EDDiscovery development team
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this
 * file except in compliance with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software distributed under
 * the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
 * ANY KIND, either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 * 
 * EDDiscovery is not affiliated with Frontier Developments plc.
 */

using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

namespace OpenTKUtils.GL4
{
    // Vertex and texture co-ords

    public class GLVertexCoordsObject : GLVertexArray
    {
        // Vertex shader must implement
        // layout(location = 0) in vec4 position;
        // layout(location = 1) in vec2 textureCoordinate;

        const int attriblayoutindexposition = 0;
        const int attriblayouttexcoord = 1;
        const int bindingindex = 0;

        GLBuffer buffer;

        public override int Count { get; set; }

        public GLVertexCoordsObject(Vector4[] vertices, Vector2[] texcoords)
        {
            Count = vertices.Length;

            System.Diagnostics.Debug.Assert(vertices.Length== texcoords.Length);

            buffer = new GLBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer.Id);

            var pos = buffer.Write(vertices, texcoords);

            GL.VertexArrayVertexBuffer(Array, bindingindex, buffer.Id, IntPtr.Zero, 0);        // tell Array that binding index comes from this buffer.

            GL.VertexArrayAttribBinding(Array, attriblayoutindexposition, bindingindex);     // bind atrib index to binding index
            GL.VertexArrayAttribBinding(Array, attriblayouttexcoord, bindingindex);     // bind atrib index to binding index

            GL.VertexAttribPointer(attriblayoutindexposition, 4, VertexAttribPointerType.Float, false, 16, pos.Item1);  // attrib 0, vertices, 4 entries, float, 16 long, at 0 in buffer
            GL.VertexAttribPointer(attriblayouttexcoord, 2, VertexAttribPointerType.Float, false, 8, pos.Item2); // attrib 1, 2 entries, float, 8 long, at offset in buffer

            GL.EnableVertexArrayAttrib(Array, attriblayoutindexposition);       // go for attrib launch!
            GL.EnableVertexArrayAttrib(Array, attriblayouttexcoord);

            GLStatics.Check();
        }

        public GLVertexCoordsObject(Tuple<Vector4[], Vector2[]> item) : this(item.Item1, item.Item2)
        {

        }

        public override void Dispose()
        {
            base.Dispose();
            buffer.Dispose();
        }
    }
}