#ifdef _DEBUG
#include <stdio.h>
#endif

#include "bass_inmemory_handler.h"
#include "inmemory_stream.h"
#include "reader.h"
#include "file_name.h"

BOOL is_initialized = FALSE;

//I have no idea how to prevent linking against this routine in msvcrt.
//It doesn't exist on Windows XP.
//Hopefully it doesn't do anything important.
int _except_handler4_common() {
	return 0;
}

BOOL BASSINMEMORYHANDLERDEF(BASS_INMEMORY_HANDLER_Init)() {
	if (is_initialized) {
		return FALSE;
	}
	is_initialized = TRUE;
#if _DEBUG
	printf("BASS INMEMORY HANDLER initialized.\n");
#endif
	return TRUE;
}

BOOL BASSINMEMORYHANDLERDEF(BASS_INMEMORY_HANDLER_Free)() {
	if (!is_initialized) {
		return FALSE;
	}
	is_initialized = FALSE;
#if _DEBUG
	printf("BASS INMEMORY HANDLER released.\n");
#endif
	return TRUE;
}

HSTREAM BASSINMEMORYHANDLERDEF(BASS_INMEMORY_HANDLER_StreamCreateFile)(BOOL mem, const void* file, QWORD offset, QWORD length, DWORD flags) {
	IM_STREAM* stream;
	BUFFER* buffer;
	const char path[MAX_PATH];
	if (!get_file_name(file, flags, path)) {
#if _DEBUG
		printf("Failed to determine file name.\n");
#endif
		return 0;
	}
	buffer = read_file_buffer(path, offset, length);
	if (buffer) {
		stream = inmemory_stream_create(buffer, &BASS_StreamCreateFileUser, flags);
		if (stream) {
			return stream->handle;
		}
	}
	return 0;
}