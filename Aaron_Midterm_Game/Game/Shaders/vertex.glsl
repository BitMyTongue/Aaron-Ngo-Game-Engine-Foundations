#version 330 core

// match Mesh.cs's order pos, uv, normal
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aNormal;

out vec3 FragPos;
out vec2 TexCoord;
out vec3 Normal;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProj;

void main()
{
    FragPos = vec3(uModel * vec4(aPos, 1.0));
    mat3 normalMatrix = mat3(transpose(inverse(uModel)));
    Normal = normalize(normalMatrix * aNormal);
    TexCoord = aTexCoord;
    gl_Position = uProj * uView * vec4(FragPos, 1.0);
}
