#version 330 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 texCoord;
layout (location = 2) in vec3 normals;

out vec2 v_TexCoord;

out vec3 FragPos;
out vec3 Normal;

//uniform mat4 u_MVP;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

void main()
{
	gl_Position = projection * view * model * position;
	v_TexCoord = texCoord;

	FragPos = vec3(model) * vec3(position);


	Normal = mat3(transpose(inverse(model))) * normals;
}